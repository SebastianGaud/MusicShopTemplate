using MusicShopTemplate.Models;
using MusicShopTemplate.Service;
using System.Collections.Generic;
using System.Windows;

namespace MusicShopTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MusicShopService service = new MusicShopService();
        public MainWindow ()
        {
            InitializeComponent();
            album_ListBox.SelectionChanged += Album_ListBox_SelectionChanged;
            gruppi_ListBox.SelectionChanged += Gruppi_ListBox_SelectionChanged;
            traccie_ListBox.SelectionChanged += Traccie_ListBox_SelectionChanged;
        }

        private void Traccie_ListBox_SelectionChanged ( object sender , System.Windows.Controls.SelectionChangedEventArgs e )
        {
            Traccia traccia = ( Traccia ) traccie_ListBox.SelectedItem;

            player.RiproduciTraccia( traccia.Titolo , traccia.AnteprimaUrlTraccia );
        }

        private void Gruppi_ListBox_SelectionChanged ( object sender , System.Windows.Controls.SelectionChangedEventArgs e )
        {
            Artista artista = ( Artista ) gruppi_ListBox.SelectedItem;
            List<Album> album = new List<Album>();

            Dictionary<string , string>[] albumCercati = service.OttieniAlbumPerArtista( artista.ID );


            /*
            for ( int i = 0 ; i < albumCercati.Length ; i++ )
            {
                album.Add( new Album()
                {
                    ID = albumCercati[ i ][ "id" ] ,
                    Cover = albumCercati[ i ][ "cover" ] ,
                    Titolo = albumCercati[ i ][ "titolo" ]
                } );
            }
            */

            foreach ( var albumCercato in albumCercati )
            {
                album.Add( new Album()
                {
                    ID = albumCercato[ "id" ] ,
                    Cover = albumCercato[ "cover" ] ,
                    Titolo = albumCercato[ "titolo" ]
                } );
            }
            album_ListBox.ItemsSource = album;
        }

        private void Album_ListBox_SelectionChanged ( object sender , System.Windows.Controls.SelectionChangedEventArgs e )
        {
            Album album = ( Album ) album_ListBox.SelectedItem;

            List<Traccia> tracce = new List<Traccia>();

            Dictionary<string , string>[] tracceCercate = service.OttieniTraccePerAlbum( album.ID );


            foreach ( var traccia in tracceCercate )
            {
                tracce.Add( new Traccia()
                {
                    ID = traccia[ "id" ] ,
                    AnteprimaUrlTraccia = traccia[ "anteprimaAudioUrl" ] ,
                    NumeroTraccia = System.Convert.ToInt32( traccia[ "numero_traccia" ] ) ,
                    Titolo = traccia[ "titolo" ]
                } );
            }


            traccie_ListBox.ItemsSource = tracce;
        }

        private void cerca_button_Click ( object sender , RoutedEventArgs e )
        {
            List<Artista> artisti = new List<Artista>();

            Dictionary<string , string>[] artistiCercati = service.CercaArtista( cerca_TextBox.Text.Trim() );

            /*
            for ( int i = 0 ; i < artistiCercati.Length ; i++ )
            {
                artisti.Add( new Artista()
                {
                    Nome = artistiCercati[ i ][ "nome" ] ,
                    ID = artistiCercati[ i ][ "id" ]
                } );
            }
            ;*/

            foreach ( var artista in artistiCercati )
            {
                artisti.Add( new Artista()
                {
                    Nome = artista[ "nome" ] ,
                    ID = artista[ "id" ]
                } );

            }

            /*
            foreach ( Artista artista in artisti )
            {
                gruppi_ListBox.Items.Add( artista.Nome );
            }
            */
            gruppi_ListBox.ItemsSource = artisti;

        }
    }
}

