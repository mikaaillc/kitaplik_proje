using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace kitaplik_proje
{
    public partial class Frmkitaplik : Form
    {
        public Frmkitaplik()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Users\asus\Desktop\Kitaplik.mdb");
        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Kitaplar", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        } 
        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand("Insert into Kitaplar (kitapad,yazar,tur,sayfa,durum) values(@p1,@p2,@p3,@p4,@p5)",baglanti);
            komut1.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            komut1.Parameters.AddWithValue("@p2", txtYazar.Text);
            komut1.Parameters.AddWithValue("@p3",cmbTur.Text);
            komut1.Parameters.AddWithValue("@p4", txtSayfa.Text);
            if (rdbtnSifir.Checked)
            {
                komut1.Parameters.AddWithValue("@p5", true);
            }
            if (rdbtnKullnilmis.Checked)
            {
                komut1.Parameters.AddWithValue("@p5", false);
            }
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtKitapAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            txtSayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
           
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                rdbtnSifir.Checked = true;
            }
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "False")
            {
                rdbtnKullnilmis.Checked = true;
            }


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
 
            OleDbCommand komut1 = new OleDbCommand("Update Kitaplar Set Kitapad=@p1 , yazar=@p2,tur=@p3,sayfa=@p4,durum=@p5 where kitapid=@p6", baglanti);
            komut1.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            komut1.Parameters.AddWithValue("@p2", txtYazar.Text);
            komut1.Parameters.AddWithValue("@p3", cmbTur.Text);
            komut1.Parameters.AddWithValue("@p4", txtSayfa.Text);
            
            if (rdbtnSifir.Checked)
            {
                komut1.Parameters.AddWithValue("@p5", true);
            }
            if (rdbtnKullnilmis.Checked)
            {
                komut1.Parameters.AddWithValue("@p5", false);
            }
            komut1.Parameters.AddWithValue("@p6", txtID.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string id = txtID.Text;
            OleDbCommand komut1 = new OleDbCommand("Delete * from Kitaplar where kitapid=@p1", baglanti);
            komut1.Parameters.AddWithValue("@p1", txtID.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(txtKitapAd.Text + "--Kitap Silindi", "Uyarı", MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
            txtID.Clear();
            txtYazar.Clear();
            txtKitapAd.Clear();
            txtSayfa.Clear();
            rdbtnKullnilmis.Checked = false;
            rdbtnSifir.Checked = false;
        }

        private void Btnbul_Click(object sender, EventArgs e)
        {
            
            OleDbCommand komut = new OleDbCommand("Select * From Kitaplar Where kitapad=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txtkitapbul.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }

        private void txtbul2_Click(object sender, EventArgs e)
        {   //like komutu
            // alanları bul
            string kt = txtkitapbul.Text;
            OleDbCommand komut = new OleDbCommand("Select * From Kitaplar Where kitapad like '%" + kt + "%'", baglanti);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }
    }
}

