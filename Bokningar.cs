namespace Labb_3
{
    internal class Bokningar
    {
        public interface IBokning
        {
            public string Namn { get; set; }
            public string Datum { get; set; }
            public string Tid { get; set; }
            public int BordNr { get; set; }
        }

        public abstract class basBokning : IBokning
        {
            public string Namn { get; set; }
            public string Datum { get; set; }
            public string Tid { get; set; }
            public int BordNr { get; set; }
            public string SparaInfo()
            {
                return $"{Namn}###{Datum}###{Tid}###{BordNr}";
            }

        }

        public class Bokning : basBokning
        {

            public Bokning(string namn, string datum, string tid, int bordnr)
            {
                this.Namn = namn;
                this.Datum = datum;
                this.Tid = tid;
                this.BordNr = bordnr;
            }
        }
    }
}
