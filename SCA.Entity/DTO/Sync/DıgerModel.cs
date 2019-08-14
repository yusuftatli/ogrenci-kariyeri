using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO.Sync
{
    public class Puanlar
    {
        public string id { get; set; }
        public string girisPuan { get; set; }
        public string yorumPuan { get; set; }
        public string paylasimPuan { get; set; }
        public string oneriPuan { get; set; }
        public string haberPuan { get; set; }
        public string testPuan { get; set; }
        public string odulBilgi { get; set; }
    }

    public class Liseler
    {
        public string id { get; set; }
        public string baslik { get; set; }
    }

    public class Universiteler
    {
        public string id { get; set; }
        public string baslik { get; set; }
    }

    public class FullKategoriler
    {
        public string id { get; set; }
        public string baslik { get; set; }
        public string alt { get; set; }
        public string ust { get; set; }
        public string tip { get; set; }
        public string foto { get; set; }
    }

    public class AltKategori
    {
        public string id { get; set; }
        public string baslik { get; set; }
    }

    public class Kategoriler
    {
        public string id { get; set; }
        public string baslik { get; set; }
        public string alt { get; set; }
        public string tip { get; set; }
        public string foto { get; set; }
        public List<AltKategori> altKategori { get; set; }
    }

    public class RootObject
    {
        public bool eskiSurum { get; set; }
        public Puanlar puanlar { get; set; }
        public List<Liseler> liseler { get; set; }
        public List<Universiteler> universiteler { get; set; }
        public bool reklam { get; set; }
        public bool fotoReklam { get; set; }
        public List<FullKategoriler> fullKategoriler { get; set; }
        public List<Kategoriler> kategoriler { get; set; }
    }
}
