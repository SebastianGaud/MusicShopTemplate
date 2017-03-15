using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MusicShopTemplate.Service
{
    public class MusicShopService : IMusicShopService
    {
        private static string CreateUri(string method,
            IEnumerable<KeyValuePair<string, string>> queryStringParams = null)
        {
            var uri = new StringBuilder("https://api.spotify.com/v1/");
            uri.Append(method);
            uri.Append($"?");

            queryStringParams
                ?.Aggregate(uri,
                    (input, kvp) =>
                    {
                        return input.Append($"&{kvp.Key}={WebUtility.UrlEncode(kvp.Value)}");
                    });

            return uri.ToString();
        }

        private JObject Get(string url)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(url).ConfigureAwait(false).GetAwaiter().GetResult())
                {
                    response.EnsureSuccessStatusCode();
                    var responseString = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<JObject>(responseString);
                }
            }
        }

        public Dictionary<string, string>[] CercaArtista(string nome)
        {
            var res = Get(CreateUri("search",
                new Dictionary<string, string>
                {
                    ["q"] = nome,
                    ["type"] = "artist",
                    ["limit"] = "10"
                }));

            var artists = (JArray)res["artists"]["items"];

            return artists
                .Select(a => new Dictionary<string, string>()
                {
                    ["id"] = a["id"].ToString(),
                    ["nome"] = a["name"].ToString()
                })
            .ToArray();
        }

        public Dictionary<string, string>[] OttieniAlbumPerArtista(string idArtista)
        {
            var res = Get(CreateUri("artists/" + idArtista + "/albums",
                new Dictionary<string, string>
                {
                    ["limit"] = "50"
                }));
            var albums = (JArray)res["items"];
            return albums
                .Where(a => a["album_type"].ToString() == "album")
                .Select(a =>
                {
                    var imagesArray = (JArray)a["images"] ?? new JArray();
                    var img300px = imagesArray.Where(i => i["height"]?.ToString() == "300").Select(i => i["url"]?.ToString()).FirstOrDefault();
                    var coverUrl = img300px ??
                            imagesArray.Select(i => i["url"]?.ToString()).FirstOrDefault();
                    var d = new Dictionary<string, string>();
                    d.Add("id", a["id"].ToString());
                    d.Add("titolo", a["name"].ToString());
                    d.Add("cover", coverUrl);
                    return d;
                })
                .ToArray();
        }

        public Dictionary<string, string>[] OttieniTraccePerAlbum(string idAlbum)
        {
            var res = Get(CreateUri("albums/" + idAlbum + "/tracks"));
            var tracks = (JArray)res["items"];
            return tracks
                .Select(t => new Dictionary<string, string>
                {
                    ["id"] = t["id"].ToString(),
                    ["titolo"] = t["name"].ToString(),
                    ["numero_traccia"] = t["track_number"].ToString(),
                    ["anteprimaAudioUrl"] = t["preview_url"]?.ToString()
                })
                .ToArray();
        }
    }
}
