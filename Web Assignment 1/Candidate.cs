
namespace Web_Assignment_1
{
    internal class Candidate
    {
        public int CandidateID { get; set; }

        public string Name { get; set; }

        public string Party { get; set; }

        public int Votes { get; set; }

        public int GenerateCandidateID()
        {
            Random rnd = new();
            return rnd.Next(1, 10000);
        }
        public Candidate()
        {
            this.Name = "";
            this.Party = "";
            this.Votes = 0;
            this.CandidateID = this.GenerateCandidateID();
        }

        public Candidate(string name, string party)
        {
            this.Name = name;
            this.Party = party;
            this.Votes = 0;
            this.CandidateID = this.GenerateCandidateID();
        }
        public void IncrementVotes()
        {
            Votes++;
        }
        public override string ToString()
        {
            return $"{CandidateID}\t\t\t{Name}\t{Party}\t\t{Votes}";
        }
    }
}