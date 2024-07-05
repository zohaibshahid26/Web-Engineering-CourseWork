using System.Text.Json;

namespace Web_Assignment_1;

internal class VotingMachineFile
{
    private readonly List<Candidate> candidates = new();

    public VotingMachineFile()
    {
        if (!File.Exists("Candidates.txt")) File.Create("Candidates.txt").Close();
        var sr = new StreamReader("Candidates.txt");
        var line = sr.ReadLine();
        var c = new Candidate();
        while (line != null)
        {
            c = JsonSerializer.Deserialize<Candidate>(line);
            candidates.Add(c);
            line = sr.ReadLine();
        }

        sr.Close();
    }

    private string inputValidater(string input, string inputType)
    {
        while (input.Length == 0)
        {
            Console.Write($"Invalid input, please enter {inputType} again: ");
            input = Console.ReadLine();
        }

        for (var i = 0; i < input.Length; i++)
            if (!char.IsLetter(input[i]) && input[i] != ' ')
            {
                Console.Write($"Invalid input, please enter {inputType} again: ");
                input = Console.ReadLine();
                i = 0;
            }

        return input;
    }

    private string ValidateCnic(string cnic)
    {
        try
        {
            while (cnic.Length != 15)
            {
                Console.WriteLine("Valid CNIC format: XXXXX-XXXXXXX-X");
                Console.Write("Invalid CNIC, please enter again: ");
                cnic = Console.ReadLine();
            }

            while (cnic[5] != '-' || cnic[13] != '-')
            {
                Console.WriteLine("Valid CNIC format: XXXXX-XXXXXXX-X");
                Console.Write("Invalid CNIC, please enter again: ");
                cnic = Console.ReadLine();
            }

            for (var i = 0; i < cnic.Length; i++)
            {
                if (i == 5 || i == 13)
                    continue;
                if (!char.IsDigit(cnic[i]))
                {
                    Console.WriteLine("Valid CNIC format: XXXXX-XXXXXXX-X");
                    Console.Write("Invalid CNIC, please enter again: ");
                    cnic = Console.ReadLine();
                    i = 0;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return cnic;
    }

    public void castVote(Voter v, Candidate c)
    {
        try
        {
            var voters = new List<Voter>();
            if (!File.Exists("Voters.txt")) File.Create("Voters.txt").Close();
            if (!File.Exists("Candidates.txt")) File.Create("Candidates.txt").Close();
            if (candidates.Count == 0)
            {
                Console.WriteLine("\nNo candidates found!\n");
                return;
            }

            var voterCnic = v.Cnic;
            v.Cnic = ValidateCnic(v.Cnic);
            var sr = new StreamReader("Voters.txt");
            var line = sr.ReadLine();
            var voter = new Voter();
            while (line != null)
            {
                voter = JsonSerializer.Deserialize<Voter>(line);
                voters.Add(voter);
                line = sr.ReadLine();
            }

            sr.Close();
            var found = false;
            foreach (var voter1 in voters)
                if (voter1.Cnic == v.Cnic)
                {
                    found = true;
                    v = voter1;
                    break;
                }

            if (!found)
            {
                Console.WriteLine("\nNo voter found with this CNIC!\n");
                Console.WriteLine("Please add voter first!\n");
                addVoter();
                var sr1 = new StreamReader("Voters.txt");
                var line1 = sr1.ReadLine();
                var voter1 = new Voter();
                voters.Clear();
                while (line1 != null)
                {
                    voter1 = JsonSerializer.Deserialize<Voter>(line1);
                    voters.Add(voter1);
                    line1 = sr1.ReadLine();
                }

                sr1.Close();
                foreach (var newVoter in voters)
                    if (newVoter.Cnic == v.Cnic)
                    {
                        v = newVoter;
                        break;
                    }
            }

            if (v.SelectedPartyName != "")
            {
                Console.WriteLine("\nYou have already casted your vote!\n");
                return;
            }

        label:
            displayCandidates();
            Console.Write("Enter CandidateID to cast vote: ");
            var id = Program.IntegerValidation(Console.ReadLine());
            var candidateID = int.Parse(id);
            var candidateFound = false;
            foreach (var candidate in candidates)
                if (candidate.CandidateID == candidateID)
                {
                    candidateFound = true;
                    c = candidate;
                    break;
                }

            if (candidateFound)
            {
                c.IncrementVotes();
                File.WriteAllText("Voters.txt", "");
                foreach (var voter1 in voters)
                {
                    if (voter1.Cnic == voterCnic) voter1.castVote(c.Party);
                    File.AppendAllText("Voters.txt", JsonSerializer.Serialize(voter1) + "\n");
                }

                File.WriteAllText("Candidates.txt", "");
                foreach (var candidate in candidates)
                {
                    var json = JsonSerializer.Serialize(candidate);
                    File.AppendAllText("Candidates.txt", json + "\n");
                }

                Console.WriteLine("\nVote casted successfully!\n");
            }
            else
            {
                Console.WriteLine("\nNo candidate found with this ID!\n");
                goto label;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void addVoter()
    {
        try
        {
            var voters = new List<Voter>();
            if (!File.Exists("Voters.txt")) File.Create("Voters.txt").Close();

            Console.Write("Enter Voter Name: ");
            var voterName = inputValidater(Console.ReadLine(), "Voter Name");
            Console.Write("Enter CNIC: ");
            var cnic = ValidateCnic(Console.ReadLine());
            var sr = new StreamReader("Voters.txt");
            var line = sr.ReadLine();
            var v = new Voter();
            while (line != null)
            {
                v = JsonSerializer.Deserialize<Voter>(line);
                if (v.Cnic == cnic)
                {
                    Console.WriteLine("Voter already exists with this CNIC!\n");
                    sr.Close();
                    return;
                }

                line = sr.ReadLine();
            }

            sr.Close();
            var newVoter = new Voter { Cnic = cnic, VoterName = voterName, SelectedPartyName = "" };
            voters.Add(newVoter);
            var json = JsonSerializer.Serialize(newVoter);
            File.AppendAllText("Voters.txt", json + "\n");
            Console.WriteLine("\nVoter added successfully!\n");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void updateVoter(string cnic)
    {
        try
        {
            cnic = ValidateCnic(cnic);
            Console.Write("Enter new Voter Name: ");
            var voterName = inputValidater(Console.ReadLine(), "Voter Name");
            var voters = new List<Voter>();
            if (!File.Exists("Voters.txt")) File.Create("Voters.txt").Close();
            var sr = new StreamReader("Voters.txt");
            var line = sr.ReadLine();
            var v = new Voter();
            while (line != null)
            {
                v = JsonSerializer.Deserialize<Voter>(line);
                voters.Add(v);
                line = sr.ReadLine();
            }

            sr.Close();
            var found = false;
            foreach (var voter in voters)
                if (voter.Cnic == cnic)
                {
                    voter.VoterName = voterName;
                    found = true;
                    break;
                }

            if (found)
            {
                File.WriteAllText("Voters.txt", "");
                foreach (var voter in voters)
                {
                    var json = JsonSerializer.Serialize(voter);
                    File.AppendAllText("Voters.txt", json + "\n");
                }

                Console.WriteLine("\nVoter updated successfully!\n");
            }
            else
            {
                Console.WriteLine("\nNo voter found with this CNIC!\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void displayVoters()
    {
        try
        {
            var voters = new List<Voter>();
            if (!File.Exists("Voters.txt")) File.Create("Voters.txt").Close();
            var sr = new StreamReader("Voters.txt");
            var line = sr.ReadLine();
            var v = new Voter();
            while (line != null)
            {
                v = JsonSerializer.Deserialize<Voter>(line);
                voters.Add(v);
                line = sr.ReadLine();
            }

            sr.Close();
            if (voters.Count == 0)
            {
                Console.WriteLine("\nNo voters found!\n");
                return;
            }

            var count = 0;
            foreach (var voter in voters) Console.WriteLine(++count + ". " + voter);
            Console.WriteLine("\n");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void deleteVoter(string cnic)
    {
        try
        {
            cnic = ValidateCnic(cnic);
            var voters = new List<Voter>();
            if (!File.Exists("Voters.txt")) File.Create("Voters.txt").Close();
            var sr = new StreamReader("Voters.txt");
            var line = sr.ReadLine();
            var v = new Voter();
            while (line != null)
            {
                v = JsonSerializer.Deserialize<Voter>(line);
                voters.Add(v);
                line = sr.ReadLine();
            }

            sr.Close();
            var found = false;
            foreach (var voter in voters)
                if (voter.Cnic == cnic)
                {
                    voters.Remove(voter);
                    found = true;
                    break;
                }

            if (found)
            {
                File.WriteAllText("Voters.txt", "");
                foreach (var voter in voters) File.AppendAllText("Voters.txt", JsonSerializer.Serialize(voter) + "\n");
                Console.WriteLine("\nVoter deleted successfully!\n");
            }
            else
            {
                Console.WriteLine("\nNo voter found with this CNIC!\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void declareWinner()
    {
        var max = 0;
        var winners = new List<Candidate>();
        foreach (var c in candidates)
            if (c.Votes >= max)
            {
                max = c.Votes;
                winners.Add(c);
            }

        if (max == 0 || winners.Count == 0)
        {
            Console.WriteLine("\nNo votes casted yet!\n");
        }
        else if (winners.Count == 1)
        {
            Console.WriteLine($"\nWinner: {winners[0].Name}  Votes: {winners[0].Votes}   Party: {winners[0].Party}");
        }
        else
        {
            Console.WriteLine("\nTie between: ");
            foreach (var c in winners) Console.WriteLine(c.Name + " Votes: " + c.Votes + "  Party: " + c.Party);
        }
    }

    public void insertCandidate()
    {
        try
        {
            if (!File.Exists("Candidates.txt")) File.Create("Candidates.txt").Close();

            Console.WriteLine("Enter candidate details: ");
            Console.Write("Name: ");
            var name = inputValidater(Console.ReadLine(), "Name");
            Console.Write("Party: ");
            var party = inputValidater(Console.ReadLine(), "Name");
            var c = new Candidate(name, party);
        label:
            foreach (var candidate in candidates)
                if (candidate.CandidateID == c.CandidateID)
                {
                    c.CandidateID = c.GenerateCandidateID();
                    goto label;
                }

            candidates.Add(c);
            File.AppendAllText("Candidates.txt", JsonSerializer.Serialize(c) + "\n");
            Console.WriteLine("\nCandidate added successfully!\n");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void readCandidate(int id)
    {
        foreach (var c in candidates)
            if (c.CandidateID == id)
            {
                Console.WriteLine("\nCandidateID\t\tName\t\tParty\t\tVotes");
                Console.WriteLine(c);
                Console.WriteLine("\n");
                return;
            }

        Console.WriteLine("No candidate found with ID " + id + "\n");
    }

    public void displayCandidates()
    {
        if (candidates.Count == 0)
        {
            Console.WriteLine("\nNo candidates found!\n");
            return;
        }

        Console.WriteLine("\nList of Candidates: \n");
        Console.WriteLine("CandidateID\t\tName\t\tParty\t\tVotes");
        foreach (var c in candidates) Console.WriteLine(c);
        Console.WriteLine("\n");
    }

    public void updateCandidate(Candidate c, int id)
    {
        try
        {
            if (candidates.Count == 0)
            {
                Console.WriteLine("\nNo candidates found!\n");
                return;
            }

            Console.Write("Enter new Name: ");
            var name = inputValidater(Console.ReadLine(), "Name");
            Console.Write("Enter new Party: ");
            var party = inputValidater(Console.ReadLine(), "Party");
            var found = false;
            c.Name = name;
            c.Party = party;
            foreach (var candidate in candidates)
                if (candidate.CandidateID == id)
                {
                    candidate.Name = c.Name;
                    candidate.Party = c.Party;
                    found = true;
                    break;
                }

            if (found)
            {
                File.WriteAllText("Candidates.txt", "");
                foreach (var candidate in candidates)
                    File.AppendAllText("Candidates.txt", JsonSerializer.Serialize(candidate) + "\n");
                Console.WriteLine("\nCandidate updated successfully!\n");
            }
            else
            {
                Console.WriteLine("\nNo candidate found with this ID!\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void deleteCandidate(int id)
    {
        try
        {
            if (candidates.Count == 0)
            {
                Console.WriteLine("\nNo candidates found!\n");
                return;
            }

            var found = false;
            foreach (var candidate in candidates)
                if (candidate.CandidateID == id)
                {
                    candidates.Remove(candidate);
                    found = true;
                    break;
                }

            if (found)
            {
                File.WriteAllText("Candidates.txt", "");
                foreach (var candidate in candidates)
                    File.AppendAllText("Candidates.txt", JsonSerializer.Serialize(candidate) + "\n");
                Console.WriteLine("\nCandidate deleted successfully!\n");
            }
            else
            {
                Console.WriteLine("\nNo candidate found with this ID!\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}