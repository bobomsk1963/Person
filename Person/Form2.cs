using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.Entity;
using DBInterface;
namespace Person
{
    public partial class Form2 : Form
    {
        IDatabase Provider;
        //dynamic 
           DBWork Context;

        public Form2(IDatabase provider, //dynamic
                                         DBWork context)
        {
            Provider = provider;
            Context = context;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Добавление 
            Persona o = Provider.CretePerson();
            using (Form3 f = new Form3(o))
            {
                f.Text = "Добавление";
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    Provider.Update(o);
                    Context.SaveChanges();
                    dataGridView1.Refresh();
                }
            }            
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Provider.GetList();
            //dataGridView1.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Изменение
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;

                dynamic s = dataGridView1.Rows[index].DataBoundItem;
                using (Form3 f = new Form3(s))
                {
                    f.Text = "Изменение";
                    bool b = f.ShowDialog(this) == DialogResult.OK;
                    Context.EntryState(s, b);
                    dataGridView1.Refresh();
                }               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Удалить
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                dynamic s = dataGridView1.Rows[index].DataBoundItem;
                Provider.Delete(s);
                Context.SaveChanges();
                dataGridView1.Refresh();                                    
            }
        }
    }
}
