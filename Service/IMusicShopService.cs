using System.Collections.Generic;

namespace MusicShopTemplate.Service
{
    public interface IMusicShopService
    {
        /// <summary>
        /// Cerca un artista.
        /// </summary>
        /// <returns>Dizionario con i seguenti campi: id, nome</returns>
        Dictionary<string, string>[] CercaArtista(string nome);

        /// <summary>
        /// Ritorna tutti gli album di un artista specifico.
        /// </summary>
        /// <param name="idArtista">id dell'artista</param>
        /// <returns>Dizionario con i seguenti campi: id, titolo, cover</returns>
        Dictionary<string, string>[] OttieniAlbumPerArtista(string idArtista);

        /// <summary>
        /// Ritorna tutte le tracce di un determinato album.
        /// </summary>
        /// <param name="idAlbum">id dell'album</param>
        /// <returns>Dizionario con i seguenti campi: id, titolo, numero_traccia, anteprimaAudioUrl</returns>
        Dictionary<string, string>[] OttieniTraccePerAlbum(string idAlbum);
    }
}