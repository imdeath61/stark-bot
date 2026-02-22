using Guna.UI2.WinForms.Suite;

namespace AcV2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // --- CONTROLES PRINCIPALES ---
        private System.Windows.Forms.Label label1; // ON/OFF
        private System.Windows.Forms.Label label2; // STARK
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2ControlBox btnCerrar;
        private Guna.UI2.WinForms.Guna2ControlBox btnMinimizar;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;

        // --- BOTÓN DE NAVEGACIÓN ENTRE PANELES ---
        private Guna.UI2.WinForms.Guna2Button btnMenu;

        // --- PANEL PRINCIPAL (CLICKER) ---
        private System.Windows.Forms.Panel panelClicker;

        // --- LEFT CLICK ---
        private System.Windows.Forms.Label lblLeftClick;
        private Guna.UI2.WinForms.Guna2Button btnLeftBind;
        private System.Windows.Forms.Label lblLeftCPSValue;
        private Guna.UI2.WinForms.Guna2TrackBar leftSlider;

        // --- RIGHT CLICK ---
        private System.Windows.Forms.Label lblRightClick;
        private Guna.UI2.WinForms.Guna2Button btnRightBind;
        private System.Windows.Forms.Label lblRightCPSValue;
        private Guna.UI2.WinForms.Guna2TrackBar rightSlider;

        // --- RANDOMIZATION ---
        private System.Windows.Forms.Label lblRandomization;
        private System.Windows.Forms.Label lblRandomValue;
        private Guna.UI2.WinForms.Guna2TrackBar randomizationSlider;
        private Guna.UI2.WinForms.Guna2ToggleSwitch btnRandomizationToggle;

        // --- PANEL DE PERFILES (GUARDAR CONFIGURACIONES) ---
        private System.Windows.Forms.Panel panelPerfiles;
        private System.Windows.Forms.Label lblPerfilesTitle;
        private System.Windows.Forms.Label lblProfileName;
        private Guna.UI2.WinForms.Guna2TextBox txtProfileName;
        private Guna.UI2.WinForms.Guna2Button btnSaveProfile;
        private Guna.UI2.WinForms.Guna2Button btnLoadProfile;
        private Guna.UI2.WinForms.Guna2Button btnDeleteProfile;
        private System.Windows.Forms.ListBox listProfiles;
        private Guna.UI2.WinForms.Guna2Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            CustomizableEdges customizableEdges9 = new CustomizableEdges();
            CustomizableEdges customizableEdges10 = new CustomizableEdges();
            CustomizableEdges customizableEdges11 = new CustomizableEdges();
            CustomizableEdges customizableEdges12 = new CustomizableEdges();
            CustomizableEdges customizableEdges13 = new CustomizableEdges();
            CustomizableEdges customizableEdges14 = new CustomizableEdges();
            CustomizableEdges customizableEdges15 = new CustomizableEdges();
            CustomizableEdges customizableEdges16 = new CustomizableEdges();
            CustomizableEdges customizableEdges17 = new CustomizableEdges();
            CustomizableEdges customizableEdges18 = new CustomizableEdges();
            CustomizableEdges customizableEdges19 = new CustomizableEdges();
            CustomizableEdges customizableEdges20 = new CustomizableEdges();
            CustomizableEdges customizableEdges21 = new CustomizableEdges();
            CustomizableEdges customizableEdges22 = new CustomizableEdges();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            btnCerrar = new Guna.UI2.WinForms.Guna2ControlBox();
            btnMinimizar = new Guna.UI2.WinForms.Guna2ControlBox();
            btnMenu = new Guna.UI2.WinForms.Guna2Button();
            panelClicker = new Panel();
            lblLeftClick = new Label();
            btnLeftBind = new Guna.UI2.WinForms.Guna2Button();
            lblLeftCPSValue = new Label();
            leftSlider = new Guna.UI2.WinForms.Guna2TrackBar();
            lblRightClick = new Label();
            btnRightBind = new Guna.UI2.WinForms.Guna2Button();
            lblRightCPSValue = new Label();
            rightSlider = new Guna.UI2.WinForms.Guna2TrackBar();
            lblRandomization = new Label();
            lblRandomValue = new Label();
            randomizationSlider = new Guna.UI2.WinForms.Guna2TrackBar();
            btnRandomizationToggle = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            panelPerfiles = new Panel();
            lblPerfilesTitle = new Label();
            lblProfileName = new Label();
            txtProfileName = new Guna.UI2.WinForms.Guna2TextBox();
            btnSaveProfile = new Guna.UI2.WinForms.Guna2Button();
            btnLoadProfile = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteProfile = new Guna.UI2.WinForms.Guna2Button();
            listProfiles = new ListBox();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            panel1.SuspendLayout();
            panelClicker.SuspendLayout();
            panelPerfiles.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(115, 470);
            label1.Name = "label1";
            label1.Size = new Size(46, 25);
            label1.TabIndex = 0;
            label1.Text = "OFF";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Black", 24F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(235, 60, 60);
            label2.Location = new Point(20, 30);
            label2.Name = "label2";
            label2.Size = new Size(126, 45);
            label2.TabIndex = 2;
            label2.Text = "STARK";
            label2.Click += label2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(20, 20, 20);
            panel1.Controls.Add(btnCerrar);
            panel1.Controls.Add(btnMinimizar);
            panel1.Controls.Add(btnMenu);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(panelClicker);
            panel1.Controls.Add(panelPerfiles);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(280, 520);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.CustomizableEdges = customizableEdges1;
            btnCerrar.FillColor = Color.Transparent;
            btnCerrar.HoverState.FillColor = Color.FromArgb(235, 60, 60);
            btnCerrar.HoverState.IconColor = Color.White;
            btnCerrar.IconColor = Color.Gray;
            btnCerrar.Location = new Point(240, 10);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCerrar.Size = new Size(30, 22);
            btnCerrar.TabIndex = 9;
            // 
            // btnMinimizar
            // 
            btnMinimizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimizar.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            btnMinimizar.CustomizableEdges = customizableEdges3;
            btnMinimizar.FillColor = Color.Transparent;
            btnMinimizar.HoverState.FillColor = Color.FromArgb(45, 45, 45);
            btnMinimizar.IconColor = Color.Gray;
            btnMinimizar.Location = new Point(205, 10);
            btnMinimizar.Name = "btnMinimizar";
            btnMinimizar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnMinimizar.Size = new Size(30, 22);
            btnMinimizar.TabIndex = 10;
            // 
            // btnMenu
            // 
            btnMenu.BorderColor = Color.FromArgb(64, 64, 64);
            btnMenu.BorderRadius = 6;
            btnMenu.BorderThickness = 1;
            btnMenu.CustomizableEdges = customizableEdges5;
            btnMenu.FillColor = Color.FromArgb(30, 30, 30);
            btnMenu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnMenu.ForeColor = Color.FromArgb(235, 60, 60);
            btnMenu.Location = new Point(155, 35);
            btnMenu.Name = "btnMenu";
            btnMenu.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnMenu.Size = new Size(40, 35);
            btnMenu.TabIndex = 11;
            btnMenu.Text = "☰";
            btnMenu.Click += BtnMenu_Click;
            // 
            // panelClicker
            // 
            panelClicker.BackColor = Color.Transparent;
            panelClicker.Controls.Add(lblLeftClick);
            panelClicker.Controls.Add(btnLeftBind);
            panelClicker.Controls.Add(lblLeftCPSValue);
            panelClicker.Controls.Add(leftSlider);
            panelClicker.Controls.Add(lblRightClick);
            panelClicker.Controls.Add(btnRightBind);
            panelClicker.Controls.Add(lblRightCPSValue);
            panelClicker.Controls.Add(rightSlider);
            panelClicker.Controls.Add(lblRandomization);
            panelClicker.Controls.Add(lblRandomValue);
            panelClicker.Controls.Add(randomizationSlider);
            panelClicker.Controls.Add(btnRandomizationToggle);
            panelClicker.Location = new Point(0, 80);
            panelClicker.Name = "panelClicker";
            panelClicker.Size = new Size(280, 370);
            panelClicker.TabIndex = 12;
            // 
            // lblLeftClick
            // 
            lblLeftClick.AutoSize = true;
            lblLeftClick.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLeftClick.ForeColor = Color.FromArgb(180, 180, 180);
            lblLeftClick.Location = new Point(20, 36);
            lblLeftClick.Name = "lblLeftClick";
            lblLeftClick.Size = new Size(80, 19);
            lblLeftClick.TabIndex = 4;
            lblLeftClick.Text = "LEFT CLICK";
            // 
            // btnLeftBind
            // 
            btnLeftBind.BorderColor = Color.FromArgb(64, 64, 64);
            btnLeftBind.BorderRadius = 6;
            btnLeftBind.BorderThickness = 1;
            btnLeftBind.CustomizableEdges = customizableEdges7;
            btnLeftBind.FillColor = Color.FromArgb(30, 30, 30);
            btnLeftBind.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLeftBind.ForeColor = Color.FromArgb(235, 60, 60);
            btnLeftBind.Location = new Point(196, 36);
            btnLeftBind.Name = "btnLeftBind";
            btnLeftBind.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnLeftBind.Size = new Size(64, 24);
            btnLeftBind.TabIndex = 5;
            btnLeftBind.Text = "None";
            btnLeftBind.Click += BtnLeftBind_Click;
            // 
            // lblLeftCPSValue
            // 
            lblLeftCPSValue.AutoSize = true;
            lblLeftCPSValue.BackColor = Color.Transparent;
            lblLeftCPSValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLeftCPSValue.ForeColor = Color.FromArgb(235, 60, 60);
            lblLeftCPSValue.Location = new Point(20, 85);
            lblLeftCPSValue.Name = "lblLeftCPSValue";
            lblLeftCPSValue.Size = new Size(31, 15);
            lblLeftCPSValue.TabIndex = 7;
            lblLeftCPSValue.Text = "17.5";
            lblLeftCPSValue.Visible = false;
            // 
            // leftSlider
            // 
            leftSlider.FillColor = Color.FromArgb(45, 45, 45);
            leftSlider.Location = new Point(20, 104);
            leftSlider.Maximum = 300;
            leftSlider.Minimum = 10;
            leftSlider.Name = "leftSlider";
            leftSlider.Size = new Size(240, 23);
            leftSlider.TabIndex = 8;
            leftSlider.ThumbColor = Color.FromArgb(235, 60, 60);
            leftSlider.Value = 175;
            leftSlider.ValueChanged += LeftSlider_ValueChanged;
            leftSlider.Scroll += LeftSlider_Scroll;
            leftSlider.MouseDown += LeftSlider_MouseDown;
            leftSlider.MouseUp += LeftSlider_MouseUp;
            // 
            // lblRightClick
            // 
            lblRightClick.AutoSize = true;
            lblRightClick.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRightClick.ForeColor = Color.FromArgb(180, 180, 180);
            lblRightClick.Location = new Point(20, 133);
            lblRightClick.Name = "lblRightClick";
            lblRightClick.Size = new Size(93, 19);
            lblRightClick.TabIndex = 9;
            lblRightClick.Text = "RIGHT CLICK";
            // 
            // btnRightBind
            // 
            btnRightBind.BorderColor = Color.FromArgb(64, 64, 64);
            btnRightBind.BorderRadius = 6;
            btnRightBind.BorderThickness = 1;
            btnRightBind.CustomizableEdges = customizableEdges9;
            btnRightBind.FillColor = Color.FromArgb(30, 30, 30);
            btnRightBind.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRightBind.ForeColor = Color.FromArgb(235, 60, 60);
            btnRightBind.Location = new Point(196, 133);
            btnRightBind.Name = "btnRightBind";
            btnRightBind.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnRightBind.Size = new Size(64, 22);
            btnRightBind.TabIndex = 10;
            btnRightBind.Text = "None";
            btnRightBind.Click += BtnRightBind_Click;
            // 
            // lblRightCPSValue
            // 
            lblRightCPSValue.AutoSize = true;
            lblRightCPSValue.BackColor = Color.Transparent;
            lblRightCPSValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRightCPSValue.ForeColor = Color.FromArgb(235, 60, 60);
            lblRightCPSValue.Location = new Point(20, 186);
            lblRightCPSValue.Name = "lblRightCPSValue";
            lblRightCPSValue.Size = new Size(31, 15);
            lblRightCPSValue.TabIndex = 12;
            lblRightCPSValue.Text = "12.5";
            lblRightCPSValue.Visible = false;
            // 
            // rightSlider
            // 
            rightSlider.FillColor = Color.FromArgb(45, 45, 45);
            rightSlider.Location = new Point(20, 204);
            rightSlider.Maximum = 300;
            rightSlider.Minimum = 10;
            rightSlider.Name = "rightSlider";
            rightSlider.Size = new Size(240, 23);
            rightSlider.TabIndex = 13;
            rightSlider.ThumbColor = Color.FromArgb(235, 60, 60);
            rightSlider.Value = 125;
            rightSlider.ValueChanged += RightSlider_ValueChanged;
            rightSlider.Scroll += RightSlider_Scroll;
            rightSlider.MouseDown += RightSlider_MouseDown;
            rightSlider.MouseUp += RightSlider_MouseUp;
            // 
            // lblRandomization
            // 
            lblRandomization.AutoSize = true;
            lblRandomization.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRandomization.ForeColor = Color.FromArgb(235, 60, 60);
            lblRandomization.Location = new Point(20, 265);
            lblRandomization.Name = "lblRandomization";
            lblRandomization.Size = new Size(101, 13);
            lblRandomization.TabIndex = 14;
            lblRandomization.Text = "RANDOMIZATION";
            // 
            // lblRandomValue
            // 
            lblRandomValue.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRandomValue.ForeColor = Color.FromArgb(255, 128, 128);
            lblRandomValue.Location = new Point(155, 262);
            lblRandomValue.Name = "lblRandomValue";
            lblRandomValue.Size = new Size(40, 19);
            lblRandomValue.TabIndex = 15;
            lblRandomValue.Text = "15%";
            lblRandomValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // randomizationSlider
            // 
            randomizationSlider.Enabled = false;
            randomizationSlider.FillColor = Color.FromArgb(45, 45, 45);
            randomizationSlider.Location = new Point(20, 284);
            randomizationSlider.Name = "randomizationSlider";
            randomizationSlider.Size = new Size(170, 23);
            randomizationSlider.TabIndex = 16;
            randomizationSlider.ThumbColor = Color.FromArgb(235, 60, 60);
            randomizationSlider.Value = 15;
            randomizationSlider.ValueChanged += RandomizationSlider_ValueChanged;
            // 
            // btnRandomizationToggle
            // 
            btnRandomizationToggle.CheckedState.BorderColor = Color.FromArgb(235, 60, 60);
            btnRandomizationToggle.CheckedState.FillColor = Color.FromArgb(235, 60, 60);
            btnRandomizationToggle.CheckedState.InnerBorderColor = Color.White;
            btnRandomizationToggle.CheckedState.InnerColor = Color.White;
            btnRandomizationToggle.CustomizableEdges = customizableEdges11;
            btnRandomizationToggle.Location = new Point(215, 284);
            btnRandomizationToggle.Name = "btnRandomizationToggle";
            btnRandomizationToggle.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnRandomizationToggle.Size = new Size(45, 23);
            btnRandomizationToggle.TabIndex = 17;
            btnRandomizationToggle.UncheckedState.BorderColor = Color.FromArgb(128, 128, 128);
            btnRandomizationToggle.UncheckedState.FillColor = Color.FromArgb(45, 45, 45);
            btnRandomizationToggle.UncheckedState.InnerBorderColor = Color.FromArgb(128, 128, 128);
            btnRandomizationToggle.UncheckedState.InnerColor = Color.FromArgb(128, 128, 128);
            btnRandomizationToggle.CheckedChanged += BtnRandomizationToggle_CheckedChanged;
            // 
            // panelPerfiles
            // 
            panelPerfiles.BackColor = Color.Transparent;
            panelPerfiles.Controls.Add(lblPerfilesTitle);
            panelPerfiles.Controls.Add(lblProfileName);
            panelPerfiles.Controls.Add(txtProfileName);
            panelPerfiles.Controls.Add(btnSaveProfile);
            panelPerfiles.Controls.Add(btnLoadProfile);
            panelPerfiles.Controls.Add(btnDeleteProfile);
            panelPerfiles.Controls.Add(listProfiles);
            panelPerfiles.Controls.Add(btnBack);
            panelPerfiles.Location = new Point(0, 80);
            panelPerfiles.Name = "panelPerfiles";
            panelPerfiles.Size = new Size(280, 370);
            panelPerfiles.TabIndex = 19;
            panelPerfiles.Visible = false;
            // 
            // lblPerfilesTitle
            // 
            lblPerfilesTitle.AutoSize = true;
            lblPerfilesTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPerfilesTitle.ForeColor = Color.FromArgb(235, 60, 60);
            lblPerfilesTitle.Location = new Point(80, 10);
            lblPerfilesTitle.Name = "lblPerfilesTitle";
            lblPerfilesTitle.Size = new Size(93, 25);
            lblPerfilesTitle.TabIndex = 0;
            lblPerfilesTitle.Text = "PERFILES";
            // 
            // lblProfileName
            // 
            lblProfileName.AutoSize = true;
            lblProfileName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProfileName.ForeColor = Color.FromArgb(180, 180, 180);
            lblProfileName.Location = new Point(20, 50);
            lblProfileName.Name = "lblProfileName";
            lblProfileName.Size = new Size(109, 15);
            lblProfileName.TabIndex = 1;
            lblProfileName.Text = "Nombre del Perfil:";
            // 
            // txtProfileName
            // 
            txtProfileName.BorderColor = Color.FromArgb(64, 64, 64);
            txtProfileName.BorderRadius = 5;
            txtProfileName.CustomizableEdges = customizableEdges13;
            txtProfileName.DefaultText = "";
            txtProfileName.FillColor = Color.FromArgb(30, 30, 30);
            txtProfileName.Font = new Font("Segoe UI", 9F);
            txtProfileName.ForeColor = Color.White;
            txtProfileName.Location = new Point(20, 70);
            txtProfileName.Name = "txtProfileName";
            txtProfileName.PlaceholderText = "Ej: PvP Mode";
            txtProfileName.SelectedText = "";
            txtProfileName.ShadowDecoration.CustomizableEdges = customizableEdges14;
            txtProfileName.Size = new Size(230, 30);
            txtProfileName.TabIndex = 2;
            // 
            // btnSaveProfile
            // 
            btnSaveProfile.BorderColor = Color.FromArgb(64, 64, 64);
            btnSaveProfile.BorderRadius = 6;
            btnSaveProfile.BorderThickness = 1;
            btnSaveProfile.CustomizableEdges = customizableEdges15;
            btnSaveProfile.FillColor = Color.FromArgb(30, 30, 30);
            btnSaveProfile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSaveProfile.ForeColor = Color.FromArgb(235, 60, 60);
            btnSaveProfile.Location = new Point(20, 110);
            btnSaveProfile.Name = "btnSaveProfile";
            btnSaveProfile.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnSaveProfile.Size = new Size(110, 28);
            btnSaveProfile.TabIndex = 3;
            btnSaveProfile.Text = "GUARDAR";
            btnSaveProfile.Click += BtnSaveProfile_Click;
            // 
            // btnLoadProfile
            // 
            btnLoadProfile.BorderColor = Color.FromArgb(64, 64, 64);
            btnLoadProfile.BorderRadius = 6;
            btnLoadProfile.BorderThickness = 1;
            btnLoadProfile.CustomizableEdges = customizableEdges17;
            btnLoadProfile.FillColor = Color.FromArgb(30, 30, 30);
            btnLoadProfile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLoadProfile.ForeColor = Color.FromArgb(235, 60, 60);
            btnLoadProfile.Location = new Point(140, 110);
            btnLoadProfile.Name = "btnLoadProfile";
            btnLoadProfile.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnLoadProfile.Size = new Size(110, 28);
            btnLoadProfile.TabIndex = 4;
            btnLoadProfile.Text = "CARGAR";
            btnLoadProfile.Click += BtnLoadProfile_Click;
            // 
            // btnDeleteProfile
            // 
            btnDeleteProfile.BorderColor = Color.FromArgb(64, 64, 64);
            btnDeleteProfile.BorderRadius = 6;
            btnDeleteProfile.BorderThickness = 1;
            btnDeleteProfile.CustomizableEdges = customizableEdges19;
            btnDeleteProfile.FillColor = Color.FromArgb(30, 30, 30);
            btnDeleteProfile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDeleteProfile.ForeColor = Color.FromArgb(235, 60, 60);
            btnDeleteProfile.Location = new Point(80, 250);
            btnDeleteProfile.Name = "btnDeleteProfile";
            btnDeleteProfile.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnDeleteProfile.Size = new Size(110, 28);
            btnDeleteProfile.TabIndex = 5;
            btnDeleteProfile.Text = "ELIMINAR";
            btnDeleteProfile.Click += BtnDeleteProfile_Click;
            // 
            // listProfiles
            // 
            listProfiles.BackColor = Color.FromArgb(30, 30, 30);
            listProfiles.BorderStyle = BorderStyle.FixedSingle;
            listProfiles.Font = new Font("Segoe UI", 9F);
            listProfiles.ForeColor = Color.White;
            listProfiles.Location = new Point(20, 150);
            listProfiles.Name = "listProfiles";
            listProfiles.Size = new Size(230, 77);
            listProfiles.TabIndex = 6;
            // 
            // btnBack
            // 
            btnBack.BorderColor = Color.FromArgb(64, 64, 64);
            btnBack.BorderRadius = 6;
            btnBack.BorderThickness = 1;
            btnBack.CustomizableEdges = customizableEdges21;
            btnBack.FillColor = Color.FromArgb(30, 30, 30);
            btnBack.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBack.ForeColor = Color.FromArgb(235, 60, 60);
            btnBack.Location = new Point(100, 300);
            btnBack.Name = "btnBack";
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnBack.Size = new Size(75, 30);
            btnBack.TabIndex = 7;
            btnBack.Text = "VOLVER";
            btnBack.Click += BtnBack_Click;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 10;
            guna2Elipse1.TargetControl = this;
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.TargetControl = panel1;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 15, 15);
            ClientSize = new Size(280, 520);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "STARK V2";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelClicker.ResumeLayout(false);
            panelClicker.PerformLayout();
            panelPerfiles.ResumeLayout(false);
            panelPerfiles.PerformLayout();
            ResumeLayout(false);
        }
    }
}