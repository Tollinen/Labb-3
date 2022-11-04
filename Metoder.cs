using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Labb_3
{
    class Metoder
    {
        public bool isFörMångaBokningar(List<Bokningar.basBokning> inBoklista, Bokningar.basBokning inBokning)
        {
            int mängdLika = 0;
            for(int i = 0; i < inBoklista.Count; i++)
            {
                if (inBoklista[i].Datum == inBokning.Datum)
                {
                    if (inBoklista[i].Tid == inBokning.Tid)
                    {
                        mängdLika++;
                        if (inBoklista[i].BordNr == inBokning.BordNr)
                        {
                            MessageBox.Show("Det valda bordet är redan bokat. Välj ett annat.");
                            return true;
                        }
                    }
                }
            }
            if(mängdLika >= 5)
            {
                MessageBox.Show($"Den {inBokning.Datum}, kl.{inBokning.Tid} är fullbokad. Välj en annan tid eller datum.");
                return true;
            }
            else
            {
                MessageBox.Show("Bokad!");
                return false;
            }
        }
    }
}
