using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;
using System.Resources;
using System.IO;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();

        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName; 
            button1.Text = Resource1.Add;
            button2.Text = Resource1.Write;
            button3.Text = Resource1.Delete;

            //listbox
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
            LoadData();

           
        }

        private void LoadData()
        {
            Flats = context.Flat.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = textBox1.Text
            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SaveFileDialog mentes = new SaveFileDialog();
            mentes.InitialDirectory = Application.StartupPath;
            mentes.Filter = "Comma Seperated Values (*.csv) | *.csv";
            mentes.DefaultExt = "csv";
            mentes.AddExtension = true;

            if (mentes.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(mentes.FileName, false, Encoding.UTF8))
            {
                foreach (var u in users)
                {
                    sw.WriteLine(u.FullName);
                    sw.Write(";");
                    sw.WriteLine(u.ID);
                    sw.Write(";");
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            var delete = listBox1.SelectedItem;
            if (delete != null)
            {
                users.Remove((User)delete);
            }
        }
    }
}
