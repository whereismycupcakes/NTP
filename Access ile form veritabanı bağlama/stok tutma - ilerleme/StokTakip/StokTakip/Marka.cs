﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace StokTakip
{
    public partial class Marka : Form
    {
        public Marka()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=StokTkp.accdb;Persist Security Info=False;");
        bool durum;
        private void markakontrol()
        {
            durum = true;
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from markabilgileri",baglanti);
            OleDbDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (comboBox1.Text==read["kategori"].ToString() && textBox1.Text == read["marka"].ToString() || comboBox1.Text == null || textBox1.Text == null) 
                {
                    durum=false;
                }
            }
            baglanti.Close();
        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            markakontrol();
            if (durum==true)
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("insert into markabilgileri(kategori,marka) values('" + comboBox1.Text + "','" + textBox1.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Marka Eklendi");
            }
            else
            {
                MessageBox.Show("Böyle bir kategori ve marka zaten var.","Uyarı");
            }
            textBox1.Text = null;
            comboBox1.Text = null;
            
        }
        private void kategorigetir()
    {

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from kategoribilgileri",baglanti);
            OleDbDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
    }
     

        private void Marka_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }
    }
}
