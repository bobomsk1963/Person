using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;
using System.Data.Entity;
using DBInterface;
namespace Person
{
    public partial class Form1 : Form
    {

        DBWork dbContext;

        dynamic ListDbSet;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {                
                IDatabase Provider = (IDatabase)comboBox1.SelectedItem;
                using (Form2 f = new Form2(Provider, dbContext))
                {
                    f.Text = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    f.ShowDialog(this);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Title = "Открыть библиотеку"; 
                op.Filter = "Библиотечный файл (*.dll)|*.dll";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    OpenDll(op.FileName);// FileName);                    
                }
            }
        }

        void OpenDll(string strname)
        {
            try
            {
                if (dbContext!=null) 
                {
                    dbContext.Close();
                    comboBox1.DataSource = null;
                }
                    textBox1.Clear();
                    label2.Text = "";
                

                Assembly asm = Assembly.LoadFrom(strname);

                // Вывод всех достурных типов
                Type[] ttt = asm.GetExportedTypes();
                for (int i = 0; i < ttt.Length; i++)
                {
                    textBox1.AppendText((i + 1) + " - " + ttt[i].Name + Environment.NewLine);
                }

                Type tContext = asm.GetType(asm.GetName().Name + ".Context", true, true);

                dynamic Context = Activator.CreateInstance(tContext);

                dbContext = new DBWork(Context);//.GetContext());                

                ListDbSet = Context.DBTable();

                comboBox1.DataSource = ListDbSet;

                if (comboBox1.Items.Count > 0) { comboBox1.SelectedIndex = 0; }

                label2.Text = strname;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
