#region #fileheader
// Sebastiano GaudeanoMusicShopTemplateMusicShopTemplateArtista.cs1511:14
#endregion
namespace MusicShopTemplate.Models
{
    public class Artista
    {
        public string ID { get; set; }
        public string Nome { get; set; }

        public override string ToString ()
        {
            return Nome;
        }
    }


}