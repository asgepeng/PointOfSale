using PointOfSale.Components;
using PointOfSale.Dialogs;
using PointOfSale.Panels;
using PointOfSale.UCs;
using System.Windows.Forms;
using System;

namespace PointOfSale
{
    public partial class MainForm : Form
    {
        private readonly Database db;
        private LoginControl loginForm;
        public MainForm()
        {
            InitializeComponent();
            db = new Database();
            loginForm = new LoginControl(db);
            loginForm.Dock = DockStyle.Fill;
        }
        internal MainPanel MainPanel { get; private set; }
        internal Database Database => db;
        private async void Form1_Load(object sender, EventArgs e)
        {
            await PointOfSale.Database.CreateDatabaseIfNotExists();
            this.Controls.Add(loginForm);
        }
        internal void LoginSuccess()
        {
            this.Controls.Remove(this.loginForm);
            if (this.loginForm != null)
            {
                this.loginForm.Dispose();
                this.loginForm = null;
            }
            this.MainPanel = new MainPanel(db);
            this.MainPanel.Dock = DockStyle.Fill;

            var settingPanel = new SettingPanel(this.MainPanel, db);
            this.Controls.Add(settingPanel);

            var headerPanel = new HeaderPanel(this.MainPanel);
            this.Controls.Add(headerPanel);

            this.Controls.Add(this.MainPanel);
            this.MainPanel.BringToFront();
            this.MainPanel.Header = headerPanel;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
