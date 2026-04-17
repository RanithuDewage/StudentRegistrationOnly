using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentRegistrationOnly
{
     public class LoginForm : Form
     {
          private PictureBox picLogo = null!;
          private GroupBox grpLogin = null!;
          private Label lblTitle = null!;
          private Label lblUsername = null!;
          private Label lblPassword = null!;
          private TextBox txtUsername = null!;
          private TextBox txtPassword = null!;
          private Button btnClear = null!;
          private Button btnLogin = null!;
          private Button btnExit = null!;

          public LoginForm()
          {
               InitializeUI();
          }

          private void InitializeUI()
          {
               Text = "Login - Skills International";
               StartPosition = FormStartPosition.CenterScreen;
               FormBorderStyle = FormBorderStyle.FixedSingle;
               MaximizeBox = false;
               BackColor = Color.WhiteSmoke;
               ClientSize = new Size(640, 560);

               picLogo = new PictureBox
               {
                    Left = 250,
                    Top = 35,
                    Width = 140,
                    Height = 110,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.None
               };

               picLogo.Image = Image.FromFile(@"C:\Users\User\OneDrive\Desktop\Kawee's C# ASP 2.0\StudentRegistrationOnly\Skills-removebg-preview.png");

               lblTitle = new Label
               {
                    Text = "Skills International",
                    Font = new Font("Segoe UI", 28, FontStyle.Bold),
                    AutoSize = true,
                    Top = 155
               };
               lblTitle.Left = (ClientSize.Width - lblTitle.PreferredSize.Width) / 2;

               grpLogin = new GroupBox
               {
                    Text = "Login",
                    Left = 140,
                    Top = 240,
                    Width = 360,
                    Height = 190
               };

               lblUsername = new Label
               {
                    Text = "Username",
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    AutoSize = true,
                    Top = 45,
                    Left = 35
               };

               txtUsername = new TextBox
               {
                    Left = 130,
                    Top = 40,
                    Width = 185
               };

               lblPassword = new Label
               {
                    Text = "Password",
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    AutoSize = true,
                    Top = 90,
                    Left = 35
               };

               txtPassword = new TextBox
               {
                    Left = 130,
                    Top = 85,
                    Width = 185,
                    PasswordChar = '*'
               };

               btnClear = new Button
               {
                    Text = "Clear",
                    Left = 35,
                    Top = 130,
                    Width = 70,
                    Height = 30
               };

               btnClear.Click += BtnClear_Click;

               btnLogin = new Button
               {
                    Text = "Login",
                    Left = 245,
                    Top = 130,
                    Width = 70,
                    Height = 30
               };

               btnLogin.Click += BtnLogin_Click;

               btnExit = new Button
               {
                    Text = "Exit",
                    Left = 20,
                    Top = 470,
                    Width = 70,
                    Height = 30
               };

               btnExit.Click += BtnExit_Click;

               grpLogin.Controls.Add(lblUsername);
               grpLogin.Controls.Add(txtUsername);
               grpLogin.Controls.Add(lblPassword);
               grpLogin.Controls.Add(txtPassword);
               grpLogin.Controls.Add(btnClear);
               grpLogin.Controls.Add(btnLogin);

               Controls.Add(picLogo);
               Controls.Add(lblTitle);
               Controls.Add(grpLogin);
               Controls.Add(btnExit);

               AcceptButton = btnLogin;
          }

          private void BtnLogin_Click(object? sender, EventArgs e)
          {
               string username = txtUsername.Text.Trim();
               string password = txtPassword.Text;

               if (username == "Admin" && password == "Skills@123")
               {
                    this.Hide();

                    using (RegistrationForm registrationForm = new RegistrationForm())
                    {
                         registrationForm.ShowDialog();
                    }

                    txtPassword.Clear();
                    txtUsername.Focus();
                    this.Show();
               }
               else
               {
                    MessageBox.Show(
                         "Invalid Login credentials, please check Username and Password and try again",
                         "Invalid Login Details",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error);

                    txtPassword.Clear();
                    txtUsername.Focus();
               }
          }

          private void BtnClear_Click(object? sender, EventArgs e)
          {
               txtUsername.Clear();
               txtPassword.Clear();
               txtUsername.Focus();
          }

          private void BtnExit_Click(object? sender, EventArgs e)
          {
               DialogResult result = MessageBox.Show(
                    "Are you Sure, Do you really want to Exit...?",
                    "Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

               if (result == DialogResult.Yes)
               {
                    Application.Exit();
               }
          }
     }
}