using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Not_Kayit_Sistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-G0BE8F7A\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True ");
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet1.TblDers' table. You can move, or remove it, as needed.
            this.tblDersTableAdapter1.Fill(this.dbNotKayitDataSet1.TblDers);
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TblDers' table. You can move, or remove it, as needed.
            this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.TblDers);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO TblDers (OgrNo,OgrAd,OgrSoyad) VALUES(@p1,@p2,@p3)", baglanti);
            cmd.Parameters.AddWithValue("@p1", msk_No.Text);
            cmd.Parameters.AddWithValue("@p2", tb_Ad.Text);
            cmd.Parameters.AddWithValue("@p3", tb_Soyad.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            //this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.TblDers);
            this.tblDersTableAdapter1.Fill(this.dbNotKayitDataSet1.TblDers);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            msk_No.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            tb_Ad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            tb_Soyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            tb_S1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            tb_S2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            tb_S3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, S1, S2, S3;
            string durum;
            S1 = Convert.ToDouble(tb_S1.Text);
            S2 = Convert.ToDouble(tb_S2.Text);
            S3 = Convert.ToDouble(tb_S3.Text);
            ortalama = (S1 + S2 + S3) / 3;
            lbl_Ortalama.Text = ortalama.ToString();

            if(ortalama>50)
            {
                durum = "True";
            }
            else
            {
                durum = "false";
            }

            baglanti.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE TblDers SET OgrS1=@P1,OgrS2=@P2,OgrS3=@P3,Ortalama=@P4,Durum=@P5 WHERE OgrNo=@P6", baglanti);
            cmd.Parameters.AddWithValue("@P1", tb_S1.Text);
            cmd.Parameters.AddWithValue("@P2", tb_S2.Text);
            cmd.Parameters.AddWithValue("@P3", tb_S3.Text);
            cmd.Parameters.AddWithValue("@P4", decimal.Parse(lbl_Ortalama.Text));
            cmd.Parameters.AddWithValue("@P5", durum);
            cmd.Parameters.AddWithValue("@P6", msk_No.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme Başarılı");
            this.tblDersTableAdapter1.Fill(this.dbNotKayitDataSet1.TblDers);

          if(ortalama>=50)
          {
                lbl_BasariDurumu.Text = "Başarılı";
          }
          else
          {
                lbl_BasariDurumu.Text = "Başarısız";
          }
        }
    }
}
