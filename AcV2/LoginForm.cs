using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Management;
using System.Net.Http;
using System.Net.Http.Json;

namespace AcV2
{
    public partial class LoginForm : Form
    {
        private TextBox txtUsername = null!;
        private TextBox txtPassword = null!;
        private Button btnLogin = null!;
        private Button btnRegister = null!;
        private Label lblTitle = null!;
        private Label lblError = null!;
        private Label lblStatus = null!;
        private bool _autenticando = false;

        public LoginForm()
        {
            InitializeComponent();
            CargarHWID();
        }

        private void InitializeComponent()
        {
            this.Text = "STARK V2 - Login";
            this.Size = new Size(350, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(20, 20, 20);
            this.ForeColor = Color.White;

            lblTitle = new Label
            {
                Text = "STARK V2",
                Font = new Font("Segoe UI Black", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(235, 60, 60),
                Location = new Point(90, 20),
                Size = new Size(200, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblStatus = new Label
            {
                Text = "Inicia sesión o regístrate",
                Location = new Point(50, 70),
                Size = new Size(250, 20),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Usuario
            Label lblUser = new Label
            {
                Text = "Usuario:",
                Location = new Point(50, 100),
                Size = new Size(250, 20),
                ForeColor = Color.White
            };

            txtUsername = new TextBox
            {
                Location = new Point(50, 120),
                Size = new Size(250, 25),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Contraseña
            Label lblPass = new Label
            {
                Text = "Contraseña:",
                Location = new Point(50, 150),
                Size = new Size(250, 20),
                ForeColor = Color.White
            };

            txtPassword = new TextBox
            {
                Location = new Point(50, 170),
                Size = new Size(250, 25),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '•'
            };

            // Botón Login
            btnLogin = new Button
            {
                Text = "INICIAR SESIÓN",
                Location = new Point(50, 210),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(235, 60, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click!;

            // Botón Registro
            btnRegister = new Button
            {
                Text = "REGISTRARSE",
                Location = new Point(180, 210),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click!;

            lblError = new Label
            {
                Location = new Point(50, 260),
                Size = new Size(250, 40),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            var lblHWID = new Label
            {
                Location = new Point(50, 300),
                Size = new Size(250, 20),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 7, FontStyle.Regular)
            };

            this.Controls.AddRange(new Control[] {
                lblTitle, lblStatus, lblUser, txtUsername, lblPass, txtPassword,
                btnLogin, btnRegister, lblError, lblHWID
            });
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MostrarError("Usuario y contraseña requeridos");
                return;
            }

            if (_autenticando) return;

            _autenticando = true;
            btnLogin.Enabled = false;
            btnRegister.Enabled = false;
            btnLogin.Text = "VERIFICANDO...";
            lblStatus.Text = "Conectando con servidor...";
            lblError.Visible = false;

            await VerificarAcceso(user, pass);
        }

        private async void BtnRegister_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MostrarError("Usuario y contraseña requeridos");
                return;
            }

            if (pass.Length < 4)
            {
                MostrarError("La contraseña debe tener al menos 4 caracteres");
                return;
            }

            if (_autenticando) return;

            _autenticando = true;
            btnLogin.Enabled = false;
            btnRegister.Enabled = false;
            btnRegister.Text = "REGISTRANDO...";
            lblStatus.Text = "Registrando usuario...";
            lblError.Visible = false;

            await RegistrarUsuario(user, pass);
        }

        private async Task VerificarAcceso(string username, string password)
        {
            try
            {
                var loginData = new
                {
                    Username = username,
                    Password = password,
                    HWID = ObtenerHWID()
                };

                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7109"); // CAMBIA ESTO A TU URL
                client.Timeout = TimeSpan.FromSeconds(5);

                var response = await client.PostAsJsonAsync("/api/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    lblStatus.Text = "✅ Acceso concedido!";
                    lblStatus.ForeColor = Color.Lime;
                    await Task.Delay(1000);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblError.Text = "❌ Usuario o contraseña incorrectos";
                    lblError.Visible = true;
                    lblStatus.Text = "Acceso denegado";
                    lblStatus.ForeColor = Color.Red;

                    btnLogin.Enabled = true;
                    btnRegister.Enabled = true;
                    btnLogin.Text = "INICIAR SESIÓN";
                    _autenticando = false;
                }
            }
            catch (Exception)
            {
                lblError.Text = "Error de conexión con el servidor.\nVerifica que la API esté corriendo.";
                lblError.Visible = true;
                lblStatus.Text = "Error de conexión";
                lblStatus.ForeColor = Color.Red;

                btnLogin.Enabled = true;
                btnRegister.Enabled = true;
                btnLogin.Text = "INICIAR SESIÓN";
                btnRegister.Text = "REGISTRARSE";
                _autenticando = false;
            }
        }

        private async Task RegistrarUsuario(string username, string password)
        {
            try
            {
                var registerData = new
                {
                    Username = username,
                    Password = password
                };

                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7109"); // CAMBIA ESTO A TU URL
                client.Timeout = TimeSpan.FromSeconds(5);

                var response = await client.PostAsJsonAsync("/api/register", registerData);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("✅ Usuario registrado correctamente.\nAhora puedes iniciar sesión.",
                        "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnRegister.Text = "REGISTRARSE";
                    btnLogin.Enabled = true;
                    btnRegister.Enabled = true;
                    lblStatus.Text = "Inicia sesión o regístrate";
                    lblStatus.ForeColor = Color.White;
                    _autenticando = false;
                    txtPassword.Clear();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    lblError.Text = error.Contains("ya existe") ? "❌ El usuario ya existe" : "❌ Error al registrar";
                    lblError.Visible = true;

                    btnRegister.Text = "REGISTRARSE";
                    btnLogin.Enabled = true;
                    btnRegister.Enabled = true;
                    _autenticando = false;
                }
            }
            catch (Exception)
            {
                lblError.Text = "Error de conexión con el servidor.\nVerifica que la API esté corriendo.";
                lblError.Visible = true;
                lblStatus.Text = "Error de conexión";
                lblStatus.ForeColor = Color.Red;

                btnLogin.Enabled = true;
                btnRegister.Enabled = true;
                btnLogin.Text = "INICIAR SESIÓN";
                btnRegister.Text = "REGISTRARSE";
                _autenticando = false;
            }
        }

        private string ObtenerHWID()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        return obj["ProcessorId"]?.ToString() ?? "UNKNOWN";
                    }
                }
            }
            catch { }
            return "UNKNOWN";
        }

        private void CargarHWID()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Label && c.Font.Size == 7)
                {
                    c.Text = $"HWID: {ObtenerHWID()}";
                }
            }
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}