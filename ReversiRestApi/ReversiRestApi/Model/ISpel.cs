﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace ReversiRestApi.Model
{
    public enum Kleur { Geen, Wit, Zwart };
    public interface ISpel
    {
        int ID { get; set; }
        string Omschrijving { get; set; }

        //het unieke token van het spel
        string Token { get; set; }
        string Speler1Token { get; set; }
        string Speler2Token { get; set; }

        Kleur[,] Bord { get; set; }
        Kleur AandeBeurt { get; set; }
        void Pas();
        bool Afgelopen();

        //welke kleur het meest voorkomend op het speelbord
        Kleur OverwegendeKleur();

        //controle of op een bepaalde positie een zet mogelijk is
        bool ZetMogelijk(int rijZet, int kolomZet);
        void DoeZet(int rijZet, int kolomZet);
    }
}
