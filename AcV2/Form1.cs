using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace AcV2
{
    public partial class Form1 : Form
    {
        // --- Importaciones de DLL ---
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        // --- Constantes de Mouse ---
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

        private const int VK_LBUTTON = 0x01;
        private const int VK_RBUTTON = 0x02;

        // --- Variables de Estado ---
        private bool leftActive = false;
        private bool rightActive = false;

        // --- LEFT CLICK ---
        private bool escuchandoLeftBind = false;
        private Keys leftBindKey = Keys.None;
        private double leftCPS = 17.5;
        private Thread leftClickerThread = null!;
        private DateTime lastLeftToggle = DateTime.Now;

        // --- RIGHT CLICK ---
        private bool escuchandoRightBind = false;
        private Keys rightBindKey = Keys.None;
        private double rightCPS = 12.5;
        private Thread rightClickerThread = null!;
        private DateTime lastRightToggle = DateTime.Now;

        // --- RANDOMIZATION ---
        private double randomizationPorcentaje = 15.0;
        private bool randomizationActiva = false;
        private Random random = new Random();

        // --- Variables para etiquetas flotantes ---
        private bool leftSliderDragging = false;
        private bool rightSliderDragging = false;

        // --- Bot de Discord ---
        private DiscordBot? _bot;

        // Colores
        private Color colorRojoStark = Color.FromArgb(235, 60, 60);
        private Color colorGrisBorde = Color.FromArgb(64, 64, 64);
        private Color colorRojoDesactivado = Color.FromArgb(255, 128, 128);

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;

            // Iniciar el bot de Discord
            string token = Environment.GetEnvironmentVariable("DISCORD_TOKEN") ?? "TOKEN_NO_ENCONTRADO";

            System.Diagnostics.Debug.WriteLine($"🔧 Verificando token... Longitud: {token?.Length ?? 0}");

            if (!string.IsNullOrEmpty(token) && token != "PON_AQUI_TU_TOKEN_DE_DISCORD")
            {
                System.Diagnostics.Debug.WriteLine($"✅ Token encontrado, creando bot...");
                _bot = new DiscordBot(token);
                System.Diagnostics.Debug.WriteLine($"🤖 Bot creado, iniciando...");

                // Iniciar el bot sin await para no bloquear
                Task.Run(async () => {
                    try
                    {
                        await _bot.IniciarAsync();
                        System.Diagnostics.Debug.WriteLine($"✅ Bot iniciado correctamente");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ Error al iniciar bot: {ex.Message}");
                    }
                });
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"❌ Token no válido o es el de ejemplo");
            }

            try
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ========== VERIFICAR LOGIN PRIMERO ==========
            using (var login = new LoginForm())
            {
                var resultado = login.ShowDialog();

                if (resultado != DialogResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
            }

            // ========== SOLO SI EL LOGIN ES EXITOSO, CONTINUAR ==========

            // Configurar valores iniciales
            leftSlider.Value = 175; // 17.5 CPS
            rightSlider.Value = 125; // 12.5 CPS

            // Estado inicial del toggle
            btnRandomizationToggle.Checked = false;
            ActualizarEstadoRandomization();

            // Iniciar hilos
            leftClickerThread = new Thread(LeftClickLoop);
            leftClickerThread.IsBackground = true;
            leftClickerThread.Priority = ThreadPriority.Highest;
            leftClickerThread.Start();

            rightClickerThread = new Thread(RightClickLoop);
            rightClickerThread.IsBackground = true;
            rightClickerThread.Priority = ThreadPriority.Highest;
            rightClickerThread.Start();

            // Cargar perfiles guardados
            CargarPerfiles();

            // Mensaje para confirmar que llegamos aquí
            System.Diagnostics.Debug.WriteLine("✅ Form1_Load completado, autoclicker listo");
        }

        // --- CLASE PARA REPRESENTAR UN PERFIL ---
        public class PerfilConfig
        {
            public string Nombre { get; set; } = "";
            public double LeftCPS { get; set; } = 17.5;
            public double RightCPS { get; set; } = 12.5;
            public string LeftBindKey { get; set; } = "None";
            public string RightBindKey { get; set; } = "None";
            public double RandomizationPorcentaje { get; set; } = 15.0;
            public bool RandomizationActiva { get; set; } = false;
        }

        // --- RUTA DE LA CARPETA DE PERFILES ---
        private string RutaPerfiles
        {
            get
            {
                string folder = Path.Combine(Application.StartupPath, "Perfiles");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                return folder;
            }
        }

        // --- BOTÓN MENÚ PARA CAMBIAR ENTRE PANELES ---
        private void BtnMenu_Click(object sender, EventArgs e)
        {
            if (panelClicker.Visible)
            {
                panelClicker.Visible = false;
                panelPerfiles.Visible = true;
                btnMenu.Text = "←";
            }
            else
            {
                panelClicker.Visible = true;
                panelPerfiles.Visible = false;
                btnMenu.Text = "☰";
            }
        }

        // --- BOTÓN VOLVER EN PANEL PERFILES ---
        private void BtnBack_Click(object sender, EventArgs e)
        {
            panelClicker.Visible = true;
            panelPerfiles.Visible = false;
            btnMenu.Text = "☰";
        }

        // --- MÉTODOS PARA PERFILES ---
        private void BtnSaveProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProfileName.Text))
            {
                MessageBox.Show("Ingresa un nombre para el perfil", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string profileName = txtProfileName.Text.Trim();

            var perfil = new PerfilConfig
            {
                Nombre = profileName,
                LeftCPS = leftCPS,
                RightCPS = rightCPS,
                LeftBindKey = leftBindKey == Keys.None ? "None" : leftBindKey.ToString(),
                RightBindKey = rightBindKey == Keys.None ? "None" : rightBindKey.ToString(),
                RandomizationPorcentaje = randomizationPorcentaje,
                RandomizationActiva = randomizationActiva
            };

            string filePath = Path.Combine(RutaPerfiles, profileName + ".json");

            if (File.Exists(filePath))
            {
                var result = MessageBox.Show("Ya existe un perfil con ese nombre. ¿Deseas sobrescribirlo?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;
            }

            try
            {
                string json = JsonSerializer.Serialize(perfil, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);

                if (!listProfiles.Items.Contains(profileName))
                    listProfiles.Items.Add(profileName);

                txtProfileName.Clear();
                MessageBox.Show($"Perfil '{profileName}' guardado correctamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el perfil: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLoadProfile_Click(object sender, EventArgs e)
        {
            if (listProfiles.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un perfil de la lista", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string profileName = listProfiles.SelectedItem.ToString();
            string filePath = Path.Combine(RutaPerfiles, profileName + ".json");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("El archivo del perfil no existe", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                var perfil = JsonSerializer.Deserialize<PerfilConfig>(json);

                if (perfil != null)
                {
                    leftCPS = perfil.LeftCPS;
                    rightCPS = perfil.RightCPS;

                    leftSlider.Value = (int)(leftCPS * 10);
                    rightSlider.Value = (int)(rightCPS * 10);

                    leftBindKey = perfil.LeftBindKey == "None" ? Keys.None : (Keys)Enum.Parse(typeof(Keys), perfil.LeftBindKey);
                    rightBindKey = perfil.RightBindKey == "None" ? Keys.None : (Keys)Enum.Parse(typeof(Keys), perfil.RightBindKey);

                    btnLeftBind.Text = perfil.LeftBindKey;
                    btnRightBind.Text = perfil.RightBindKey;

                    randomizationPorcentaje = perfil.RandomizationPorcentaje;
                    randomizationActiva = perfil.RandomizationActiva;

                    randomizationSlider.Value = (int)randomizationPorcentaje;
                    btnRandomizationToggle.Checked = randomizationActiva;

                    lblRandomValue.Text = randomizationPorcentaje.ToString("F0") + "%";
                    ActualizarEstadoRandomization();

                    MessageBox.Show($"Perfil '{profileName}' cargado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    panelClicker.Visible = true;
                    panelPerfiles.Visible = false;
                    btnMenu.Text = "☰";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el perfil: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteProfile_Click(object sender, EventArgs e)
        {
            if (listProfiles.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un perfil de la lista", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string profileName = listProfiles.SelectedItem.ToString();
            string filePath = Path.Combine(RutaPerfiles, profileName + ".json");

            var result = MessageBox.Show($"¿Estás seguro de eliminar el perfil '{profileName}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    listProfiles.Items.Remove(listProfiles.SelectedItem);

                    MessageBox.Show($"Perfil '{profileName}' eliminado", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el perfil: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarPerfiles()
        {
            try
            {
                listProfiles.Items.Clear();

                if (Directory.Exists(RutaPerfiles))
                {
                    var files = Directory.GetFiles(RutaPerfiles, "*.json");
                    foreach (var file in files)
                    {
                        string profileName = Path.GetFileNameWithoutExtension(file);
                        listProfiles.Items.Add(profileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar perfiles: {ex.Message}");
            }
        }

        // --- LEFT CLICK EVENTS ---
        private void BtnLeftBind_Click(object sender, EventArgs e)
        {
            escuchandoLeftBind = true;
            btnLeftBind.Text = "...";
            btnLeftBind.BorderColor = colorRojoStark;
            this.Focus();
        }

        private void LeftSlider_ValueChanged(object sender, EventArgs e)
        {
            leftCPS = (double)leftSlider.Value / 10.0;
            if (leftSliderDragging)
            {
                ActualizarPosicionLeftCPSValue();
            }
        }

        // --- RIGHT CLICK EVENTS ---
        private void BtnRightBind_Click(object sender, EventArgs e)
        {
            escuchandoRightBind = true;
            btnRightBind.Text = "...";
            btnRightBind.BorderColor = colorRojoStark;
            this.Focus();
        }

        private void RightSlider_ValueChanged(object sender, EventArgs e)
        {
            rightCPS = (double)rightSlider.Value / 10.0;
            if (rightSliderDragging)
            {
                ActualizarPosicionRightCPSValue();
            }
        }

        // --- RANDOMIZATION EVENTS ---
        private void RandomizationSlider_ValueChanged(object sender, EventArgs e)
        {
            randomizationPorcentaje = randomizationSlider.Value;
            lblRandomValue.Text = randomizationPorcentaje.ToString("F0") + "%";
        }

        private void BtnRandomizationToggle_CheckedChanged(object sender, EventArgs e)
        {
            randomizationActiva = btnRandomizationToggle.Checked;
            ActualizarEstadoRandomization();
        }

        private void ActualizarEstadoRandomization()
        {
            if (randomizationActiva)
            {
                lblRandomValue.ForeColor = colorRojoStark;
                randomizationSlider.Enabled = true;
            }
            else
            {
                lblRandomValue.ForeColor = colorRojoDesactivado;
                randomizationSlider.Enabled = false;
            }
        }

        // --- KEYBOARD HANDLING ---
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (escuchandoLeftBind)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    leftBindKey = Keys.None;
                    btnLeftBind.Text = "None";
                }
                else
                {
                    leftBindKey = e.KeyCode;
                    btnLeftBind.Text = leftBindKey.ToString().ToUpper();
                }
                btnLeftBind.BorderColor = colorGrisBorde;
                escuchandoLeftBind = false;
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (escuchandoRightBind)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    rightBindKey = Keys.None;
                    btnRightBind.Text = "None";
                }
                else
                {
                    rightBindKey = e.KeyCode;
                    btnRightBind.Text = rightBindKey.ToString().ToUpper();
                }
                btnRightBind.BorderColor = colorGrisBorde;
                escuchandoRightBind = false;
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            base.OnKeyDown(e);
        }

        // --- GAME DETECTION ---
        private bool IsGameActive()
        {
            IntPtr handle = GetForegroundWindow();
            StringBuilder sb = new StringBuilder(256);
            if (GetWindowText(handle, sb, 256) > 0)
            {
                string title = sb.ToString().ToLower();
                return title.Contains("minecraft") ||
                       title.Contains("lunar") ||
                       title.Contains("badlion") ||
                       title.Contains("javaw") ||
                       title.Contains("cheatbreaker");
            }
            return false;
        }

        // --- STATUS UPDATE ---
        private void UpdateStatus()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateStatus()));
                return;
            }

            if (leftActive || rightActive)
            {
                label1.Text = "ON";
                label1.ForeColor = Color.Lime;
            }
            else
            {
                label1.Text = "OFF";
                label1.ForeColor = Color.Red;
            }
        }

        // --- CALCULAR INTERVALO CON RANDOM ---
        private double CalcularIntervaloRandom(double cps)
        {
            double intervaloBase = 1000.0 / cps;

            if (randomizationActiva && randomizationPorcentaje > 0)
            {
                double variacionMaxima = intervaloBase * (randomizationPorcentaje / 100.0);
                double variacion = (random.NextDouble() * 2 - 1) * variacionMaxima;
                return intervaloBase + variacion;
            }

            return intervaloBase;
        }

        // --- LEFT CLICK LOOP ---
        private void LeftClickLoop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            double proximoIntervalo = CalcularIntervaloRandom(leftCPS);

            while (true)
            {
                try
                {
                    if (leftBindKey != Keys.None && !escuchandoLeftBind)
                    {
                        if ((GetAsyncKeyState((int)leftBindKey) & 0x8000) != 0)
                        {
                            if ((DateTime.Now - lastLeftToggle).TotalMilliseconds > 250)
                            {
                                leftActive = !leftActive;
                                UpdateStatus();
                                lastLeftToggle = DateTime.Now;
                            }
                        }
                    }

                    if (leftActive && (GetAsyncKeyState(VK_LBUTTON) & 0x8000) != 0 && IsGameActive())
                    {
                        if (sw.Elapsed.TotalMilliseconds >= proximoIntervalo)
                        {
                            sw.Restart();
                            proximoIntervalo = CalcularIntervaloRandom(leftCPS);

                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        }
                        Thread.SpinWait(100);
                    }
                    else
                    {
                        Thread.Sleep(10);
                        if (!leftActive)
                        {
                            proximoIntervalo = CalcularIntervaloRandom(leftCPS);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error Left: " + ex.Message);
                    Thread.Sleep(100);
                }
            }
        }

        // --- RIGHT CLICK LOOP ---
        private void RightClickLoop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            double proximoIntervalo = CalcularIntervaloRandom(rightCPS);

            while (true)
            {
                try
                {
                    if (rightBindKey != Keys.None && !escuchandoRightBind)
                    {
                        if ((GetAsyncKeyState((int)rightBindKey) & 0x8000) != 0)
                        {
                            if ((DateTime.Now - lastRightToggle).TotalMilliseconds > 250)
                            {
                                rightActive = !rightActive;
                                UpdateStatus();
                                lastRightToggle = DateTime.Now;
                            }
                        }
                    }

                    if (rightActive && (GetAsyncKeyState(VK_RBUTTON) & 0x8000) != 0 && IsGameActive())
                    {
                        if (sw.Elapsed.TotalMilliseconds >= proximoIntervalo)
                        {
                            sw.Restart();
                            proximoIntervalo = CalcularIntervaloRandom(rightCPS);

                            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                        }
                        Thread.SpinWait(100);
                    }
                    else
                    {
                        Thread.Sleep(10);
                        if (!rightActive)
                        {
                            proximoIntervalo = CalcularIntervaloRandom(rightCPS);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error Right: " + ex.Message);
                    Thread.Sleep(100);
                }
            }
        }

        // --- EVENTOS PARA ETIQUETAS FLOTANTES IZQUIERDO ---
        private void LeftSlider_MouseDown(object sender, MouseEventArgs e)
        {
            leftSliderDragging = true;
            ActualizarPosicionLeftCPSValue();
            lblLeftCPSValue.Visible = true;
        }

        private void LeftSlider_MouseUp(object sender, MouseEventArgs e)
        {
            leftSliderDragging = false;
            lblLeftCPSValue.Visible = false;
        }

        private void LeftSlider_Scroll(object sender, ScrollEventArgs e)
        {
            if (leftSliderDragging)
            {
                ActualizarPosicionLeftCPSValue();
            }
        }

        private void ActualizarPosicionLeftCPSValue()
        {
            int sliderMin = leftSlider.Minimum;
            int sliderMax = leftSlider.Maximum;
            int sliderValue = leftSlider.Value;
            int sliderWidth = leftSlider.Width;

            double porcentaje = (double)(sliderValue - sliderMin) / (sliderMax - sliderMin);
            int posX = leftSlider.Left + (int)(porcentaje * sliderWidth) - (lblLeftCPSValue.Width / 2);

            posX = Math.Max(leftSlider.Left, Math.Min(leftSlider.Left + sliderWidth - lblLeftCPSValue.Width, posX));

            lblLeftCPSValue.Left = posX;
            lblLeftCPSValue.Top = leftSlider.Top - 20;
            lblLeftCPSValue.Text = leftCPS.ToString("F1");
        }

        // --- EVENTOS PARA ETIQUETAS FLOTANTES DERECHO ---
        private void RightSlider_MouseDown(object sender, MouseEventArgs e)
        {
            rightSliderDragging = true;
            ActualizarPosicionRightCPSValue();
            lblRightCPSValue.Visible = true;
        }

        private void RightSlider_MouseUp(object sender, MouseEventArgs e)
        {
            rightSliderDragging = false;
            lblRightCPSValue.Visible = false;
        }

        private void RightSlider_Scroll(object sender, ScrollEventArgs e)
        {
            if (rightSliderDragging)
            {
                ActualizarPosicionRightCPSValue();
            }
        }

        private void ActualizarPosicionRightCPSValue()
        {
            int sliderMin = rightSlider.Minimum;
            int sliderMax = rightSlider.Maximum;
            int sliderValue = rightSlider.Value;
            int sliderWidth = rightSlider.Width;

            double porcentaje = (double)(sliderValue - sliderMin) / (sliderMax - sliderMin);
            int posX = rightSlider.Left + (int)(porcentaje * sliderWidth) - (lblRightCPSValue.Width / 2);

            posX = Math.Max(rightSlider.Left, Math.Min(rightSlider.Left + sliderWidth - lblRightCPSValue.Width, posX));

            lblRightCPSValue.Left = posX;
            lblRightCPSValue.Top = rightSlider.Top - 20;
            lblRightCPSValue.Text = rightCPS.ToString("F1");
        }

        // --- PROTECCIÓN AL CERRAR EL FORMULARIO ---
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            base.OnFormClosing(e);
        }

        // --- DUMMY METHODS ---
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}