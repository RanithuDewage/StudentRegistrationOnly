using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace StudentRegistrationOnly
{
     public class RegistrationForm : Form
     {
          private ComboBox cmbRegNo = null!;
          private TextBox txtFirstName = null!;
          private TextBox txtLastName = null!;
          private DateTimePicker dtpDOB = null!;
          private RadioButton rbMale = null!;
          private RadioButton rbFemale = null!;
          private TextBox txtAddress = null!;
          private TextBox txtEmail = null!;
          private TextBox txtMobilePhone = null!;
          private TextBox txtHomePhone = null!;
          private TextBox txtParentName = null!;
          private TextBox txtNIC = null!;
          private TextBox txtContactNo = null!;

          private Button btnRegister = null!;
          private Button btnUpdate = null!;
          private Button btnClear = null!;
          private Button btnDelete = null!;

          private LinkLabel lnkLogout = null!;
          private LinkLabel lnkExit = null!;

          private GroupBox grpStudentRegistration = null!;
          private GroupBox grpBasicDetails = null!;
          private GroupBox grpContactDetails = null!;
          private GroupBox grpParentDetails = null!;

          public RegistrationForm()
          {
               InitializeUI();
               Load += RegistrationForm_Load;
          }

          private Label MakeLabel(string text, int  x, int y)
          {
               return new Label
               {
                    Text = text,
                    Left = x,
                    Top = y,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10, FontStyle.Regular)
               };
          }

          private TextBox MakeTextBox(int x, int y, int width = 190)
          {
               return new TextBox
               {
                    Left = x,
                    Top = y,
                    Width = width
               };
          }

          private void StyleButton(Button button, Color backColor)
          {
               button.BackColor = backColor;
               button.ForeColor = Color.White;
               button.FlatStyle = FlatStyle.Flat;
               button.FlatAppearance.BorderSize = 0;
          }

          private void InitializeUI()
          {
               Text = "Student Registration - Skills International";
               StartPosition = FormStartPosition.CenterScreen;
               FormBorderStyle = FormBorderStyle.FixedSingle;
               MaximizeBox = false;
               BackColor = Color.WhiteSmoke;
               AutoScroll = true;
               ClientSize = new Size(820, 830);

               lnkLogout = new LinkLabel
               {
                    Text = "Logout",
                    Left = 15,
                    Top = 28,
                    AutoSize = true,
                    LinkColor = Color.Blue
               };
               lnkLogout.LinkClicked += LnkLogout_LinkClicked;

               Label lblTitle = new Label
               {
                    Text = "Skills International",
                    Font = new Font("Segoe UI", 28, FontStyle.Bold),
                    AutoSize = true,
                    Top = 10
               };
               lblTitle.Left = (ClientSize.Width - lblTitle.PreferredSize.Width) / 2;

               grpStudentRegistration = new GroupBox
               {
                    Text = "Student Registration",
                    Left = 8,
                    Top = 70,
                    Width = 785,
                    Height = 715
               };

               Label lblRegNo = MakeLabel("Reg No", 75, 35);

               cmbRegNo = new ComboBox
               {
                    Left = 180,
                    Top = 30,
                    Width = 150,
                    DropDownStyle = ComboBoxStyle.DropDown
               };
               cmbRegNo.SelectedIndexChanged += (s, e) => LoadStudentByRegNo();
               cmbRegNo.Leave += (s, e) => LoadStudentByRegNo();

               grpBasicDetails = new GroupBox
               {
                    Text = "Basic Details",
                    Left = 35,
                    Top = 75,
                    Width = 690,
                    Height = 210,
               };

               txtFirstName = MakeTextBox(140, 30, 410);
               txtLastName = MakeTextBox(140, 75, 410);

               dtpDOB = new DateTimePicker
               {
                    Left = 140,
                    Top = 120,
                    Width = 175,
                    Format = DateTimePickerFormat.Short,
                    Value = new DateTime(2015, 1, 1)
               };

               rbMale = new RadioButton
               {
                    Text = "Male",
                    Left = 140,
                    Top = 160,
                    AutoSize = true
               };

               rbFemale = new RadioButton
               {
                    Text = "Female",
                    Left = 260,
                    Top = 160,
                    AutoSize = true
               };

               grpBasicDetails.Controls.Add(MakeLabel("First Name", 30, 35));
               grpBasicDetails.Controls.Add(txtFirstName);
               
               grpBasicDetails.Controls.Add(MakeLabel("Last Name", 30, 80));
               grpBasicDetails.Controls.Add(txtLastName);

               grpBasicDetails.Controls.Add(MakeLabel("Date of Birth", 30, 125));
               grpBasicDetails.Controls.Add(dtpDOB);

               grpBasicDetails.Controls.Add(MakeLabel("Gender", 30, 160));
               grpBasicDetails.Controls.Add(rbMale);
               grpBasicDetails.Controls.Add(rbFemale);

               grpContactDetails = new GroupBox
               {
                    Text = "Contact Details",
                    Left = 35,
                    Top = 300,
                    Width = 690,
                    Height = 190
               };

               txtAddress = MakeTextBox(140, 25, 410);
               txtAddress.Multiline = true;
               txtAddress.Height = 60;

               txtEmail = MakeTextBox(140, 105, 410);
               txtMobilePhone = MakeTextBox(140, 145, 120);
               txtHomePhone = MakeTextBox(440, 145, 120);

               grpContactDetails.Controls.Add(MakeLabel("Address", 30, 30));
               grpContactDetails.Controls.Add(txtAddress);

               grpContactDetails.Controls.Add(MakeLabel("Email", 30, 110));
               grpContactDetails.Controls.Add(txtEmail);

               grpContactDetails.Controls.Add(MakeLabel("Mobile Phone", 30, 150));
               grpContactDetails.Controls.Add(txtMobilePhone);

               grpContactDetails.Controls.Add(MakeLabel("Home Phone", 330, 150));
               grpContactDetails.Controls.Add(txtHomePhone);

               grpParentDetails = new GroupBox
               {
                    Text = "Parent Details",
                    Left = 35,
                    Top = 510,
                    Width = 690,
                    Height = 140,
               };

               txtParentName = MakeTextBox(140, 25, 410);
               txtNIC = MakeTextBox(140, 70, 150);
               txtContactNo = MakeTextBox(140, 110, 150);

               grpParentDetails.Controls.Add(MakeLabel("Parent Name", 30, 30));
               grpParentDetails.Controls.Add(txtParentName);

               grpParentDetails.Controls.Add(MakeLabel("NIC", 30, 75));
               grpParentDetails.Controls.Add(txtNIC);

               grpParentDetails.Controls.Add(MakeLabel("Contact Number", 30, 115));
               grpParentDetails.Controls.Add(txtContactNo);

               btnRegister = new Button
               {
                    Text = "Register",
                    Left = 35,
                    Top = 670,
                    Width = 75,
                    Height = 32,
               };
               StyleButton(btnRegister, Color.SeaGreen);
               btnRegister.Click += BtnRegister_Click;

               btnUpdate = new Button
               {
                    Text = "Update",
                    Left = 120,
                    Top = 670,
                    Width = 75,
                    Height = 32
               };
               StyleButton(btnUpdate, Color.DarkOrange);
               btnUpdate.Click += BtnUpdate_Click;

               btnClear = new Button
               {
                    Text = "Clear",
                    Left = 565,
                    Top = 670,
                    Width = 75,
                    Height = 32
               };
               StyleButton(btnClear, Color.SteelBlue);
               btnClear.Click += BtnClear_Click;

               btnDelete = new Button
               {
                    Text = "Delete",
                    Left = 650,
                    Top = 670,
                    Width = 75,
                    Height = 32
               };
               StyleButton(btnDelete, Color.Firebrick);
               btnDelete.Click += BtnDelete_Click;

               grpStudentRegistration.Controls.Add(lblRegNo);
               grpStudentRegistration.Controls.Add(cmbRegNo);
               grpStudentRegistration.Controls.Add(grpBasicDetails);
               grpStudentRegistration.Controls.Add(grpContactDetails);
               grpStudentRegistration.Controls.Add(grpParentDetails);
               grpStudentRegistration.Controls.Add(btnRegister);
               grpStudentRegistration.Controls.Add(btnUpdate);
               grpStudentRegistration.Controls.Add(btnClear);
               grpStudentRegistration.Controls.Add(btnDelete);

               lnkExit = new LinkLabel
               {
                    Text = "Exit",
                    AutoSize = true,
                    LinkColor = Color.Blue,
                    Top = 795
               };
               lnkExit.Left = ClientSize.Width - lnkExit.PreferredSize.Width - 15;
               lnkExit.LinkClicked += LnkExit_LinkClicked;

               Controls.Add(lnkLogout);
               Controls.Add(lblTitle);
               Controls.Add(grpStudentRegistration);
               Controls.Add(lnkExit);
          }

          private void RegistrationForm_Load(object? sender, EventArgs e)
          {
               LoadRegNos();
          }

          private string GetGender()
          {
               if (rbMale.Checked) return "Male";
               if (rbFemale.Checked) return "Female";
               return string.Empty;
          }

          private void SetGender(string gender)
          {
               rbMale.Checked = string.Equals(gender, "Male", StringComparison.OrdinalIgnoreCase);
               rbFemale.Checked = string.Equals(gender, "Female", StringComparison.OrdinalIgnoreCase);
          }

          private void ClearFields(bool clearRegNo = true)
          {
               if (clearRegNo)
                    cmbRegNo.Text = string.Empty;
               txtFirstName.Clear();
               txtLastName.Clear();
               dtpDOB.Value = new DateTime(2015, 1, 1);
               rbMale.Checked = false;
               rbFemale.Checked = false;
               txtAddress.Clear();
               txtEmail.Clear();
               txtMobilePhone.Clear();
               txtHomePhone.Clear();
               txtParentName.Clear();
               txtNIC.Clear();
               txtContactNo.Clear();
          }

          private bool ValidateInputs()
          {
               if (!int.TryParse(cmbRegNo.Text.Trim(), out _))
               {
                    MessageBox.Show("Please Enter Valid Reg NO.");
                    cmbRegNo.Focus();
                    return false;
               }

               if (string.IsNullOrWhiteSpace(txtFirstName.Text))
               {
                    MessageBox.Show("Please Enter First Name.");
                    txtFirstName.Focus();
                    return false;
               }

               if (string.IsNullOrWhiteSpace(txtLastName.Text))
               {
                    MessageBox.Show("Please Enter Last Name.");
                    txtLastName.Focus();
                    return false;
               }

               if (GetGender() == string.Empty)
               {
                    MessageBox.Show("Please Select Gender.");
                    return false;
               }

               if (string.IsNullOrWhiteSpace(txtMobilePhone.Text) || !long.TryParse(txtMobilePhone.Text.Trim(), out _))
               {
                    MessageBox.Show("Please Enter Valid Mobile Phone No.");
                    txtMobilePhone.Focus();
                    return false;
               }

               if (string.IsNullOrWhiteSpace(txtHomePhone.Text) || !long.TryParse(txtHomePhone.Text.Trim(), out _))
               {
                    MessageBox.Show("Please Enter Valid Home Phone No.");
                    txtHomePhone.Focus();
                    return false;
               }

               if (string.IsNullOrWhiteSpace(txtContactNo.Text) || !long.TryParse(txtContactNo.Text.Trim(), out _))
               {
                    MessageBox.Show("Please Enter Valid Contact No.");
                    txtContactNo.Focus();
                    return false;
               }

               return true;
          }

          private void FillParameters(SqlCommand cmd)
          {
               cmd.Parameters.AddWithValue("@regNo", int.Parse(cmbRegNo.Text.Trim()));
               cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text.Trim());
               cmd.Parameters.AddWithValue("@lastName", txtLastName.Text.Trim());
               cmd.Parameters.AddWithValue("@dateOfBirth", dtpDOB.Value.Date);
               cmd.Parameters.AddWithValue("@gender", GetGender());
               cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
               cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
               cmd.Parameters.AddWithValue("@mobilePhone", long.Parse(txtMobilePhone.Text.Trim()));
               cmd.Parameters.AddWithValue("@homePhone", long.Parse(txtHomePhone.Text.Trim()));
               cmd.Parameters.AddWithValue("@parentName", txtParentName.Text.Trim());
               cmd.Parameters.AddWithValue("@nic", txtNIC.Text.Trim());
               cmd.Parameters.AddWithValue("@contactNo", long.Parse(txtContactNo.Text.Trim()));
          }

          private void LoadRegNos()
          {
               try
               {
                    cmbRegNo.Items.Clear();

                    using (SqlConnection con = DB.GetConnection())
                    {
                         string sql = "SELECT regNo FROM Registration ORDER BY regNo";

                         using (SqlCommand cmd = new SqlCommand(sql, con))
                         {
                              con.Open();

                              using (SqlDataReader dr = cmd.ExecuteReader())
                              {
                                   while (dr.Read())
                                   {
                                        cmbRegNo.Items.Add(dr["regNo"]?.ToString() ?? string.Empty);
                                   }
                              }
                         }
                    }
               }
               catch (Exception ex)
               {
                    MessageBox.Show("LoadRegNos Error: " + ex.Message);
               }
          }

          private void LoadStudentByRegNo()
          {
               if (!int.TryParse(cmbRegNo.Text.Trim(), out int regNo))
                    return;
               try
               {
                    using (SqlConnection con = DB.GetConnection())
                    {
                         string sql = "SELECT * FROM Registration WHERE regNo = @regNo";

                         using (SqlCommand cmd = new SqlCommand(sql, con))
                         {
                              cmd.Parameters.AddWithValue("@regNo", regNo);
                              con.Open();

                              using (SqlDataReader dr = cmd.ExecuteReader())
                              {
                                   if (dr.Read())
                                   {
                                        txtFirstName.Text = dr["firstName"]?.ToString() ?? string.Empty;
                                        txtLastName.Text = dr["lastName"]?.ToString() ?? string.Empty;

                                        if (dr["dateOfBirth"] != DBNull.Value)
                                             dtpDOB.Value = Convert.ToDateTime(dr["dateOfBirth"]);
                                        else
                                             dtpDOB.Value = new DateTime(2015, 1, 1);

                                        SetGender(dr["gender"]?.ToString() ?? string.Empty);
                                        txtAddress.Text = dr["address"]?.ToString() ?? string.Empty;
                                        txtEmail.Text = dr["email"]?.ToString() ?? string.Empty;
                                        txtMobilePhone.Text = dr["mobilePhone"]?.ToString() ?? string.Empty;
                                        txtHomePhone.Text = dr["homePhone"]?.ToString() ?? string.Empty;
                                        txtParentName.Text = dr["parentName"]?.ToString() ?? string.Empty;
                                        txtNIC.Text = dr["nic"]?.ToString() ?? string.Empty;
                                        txtContactNo.Text = dr["contactNo"]?.ToString() ?? string.Empty;
                                   }
                                   else
                                   {
                                        ClearFields(false);
                                   }
                              }
                         }
                    }
               }
               catch (Exception ex)
               {
                    MessageBox.Show("LoadStudent Error: " + ex.Message);
               }
          }

          private void BtnRegister_Click(object? sender, EventArgs e)
          {
               if (!ValidateInputs())
                    return;
               try
               {
                    using (SqlConnection con = DB.GetConnection())
                    {
                         string sql = @"INSERT INTO Registration
                                        (regNo, firstName, lastName, dateOfBirth, gender, address, email, mobilePhone, homePhone, parentName, nic, contactNo)
                                        VALUES
                                        (@regNo, @firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @nic, @contactNo)";

                         using (SqlCommand cmd = new SqlCommand(sql, con))
                         {
                              FillParameters(cmd);
                              con.Open();
                              cmd.ExecuteNonQuery();
                         }
                    }

                    MessageBox.Show("Record Added Successfully.");
                    LoadRegNos();
                    ClearFields();
               }
               catch (Exception ex)
               {
                    MessageBox.Show("Register Error: " + ex.Message);
               }
          }

          private void BtnUpdate_Click(object? sender, EventArgs e)
          {
               if (!ValidateInputs())
                    return;
               try
               {
                    using (SqlConnection con = DB.GetConnection())
                    {
                         string sql = @"UPDATE Registration SET
                                        firstName = @firstName,
                                        lastName = @lastName,
                                        dateOfBirth = @dateOfBirth,
                                        gender = @gender,
                                        address = @address,
                                        email = @email,
                                        mobilePhone = @mobilePhone,
                                        homePhone = @homePhone,
                                        parentName = @parentName,
                                        nic = @nic,
                                        contactNo = @contactNo
                                        WHERE regNo = @regNo";

                         using (SqlCommand cmd = new SqlCommand(sql, con))
                         {
                              FillParameters(cmd);
                              con.Open();
                              int rows = cmd.ExecuteNonQuery();

                              if (rows > 0)
                                   MessageBox.Show("Record Updated Successfully.");
                              else
                                   MessageBox.Show("Record Not Found.");
                         }    
                    }

                    LoadRegNos();
               }
               catch (Exception ex)
               {
                    MessageBox.Show("Update Error: " + ex.Message);
               }     
          }

          private void BtnClear_Click(object? sender, EventArgs e)
          {
               ClearFields();
          }

          private void BtnDelete_Click(object? sender, EventArgs e)
          {
               if (!int.TryParse(cmbRegNo.Text.Trim(), out int regNo))
               {
                    MessageBox.Show("Please Select Valid Reg No.");
                    return;
               }

               DialogResult result = MessageBox.Show(
                    "Are you Sure you Really Want to Delete this Record?",
                    "Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

               if (result != DialogResult.Yes)
                    return;

               try
               {
                    using (SqlConnection con = DB.GetConnection())
                    {
                         string sql = "DELETE FROM Registration WHERE regNo = @regNo";

                         using (SqlCommand cmd = new SqlCommand(sql, con))
                         {
                              cmd.Parameters.AddWithValue("@regNo", regNo);
                              con.Open();

                              int rows = cmd.ExecuteNonQuery();

                              if (rows > 0)
                              {
                                   MessageBox.Show("Record Deleted Successfully.");
                                   LoadRegNos();
                                   ClearFields();
                              }
                              else
                              {
                                   MessageBox.Show("Record Not Found.");
                              }
                         }
                    }
               }
               catch (Exception ex)
               {
                    MessageBox.Show("Delete Error: " + ex.Message);
               }
          }

          private void LnkLogout_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
          {
               DialogResult result = MessageBox.Show(
                    "Are you Sure you want to Logout?",
                    "Logout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    
               if (result == DialogResult.Yes)
               {
                    Close();
               }
          }

          private void LnkExit_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
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