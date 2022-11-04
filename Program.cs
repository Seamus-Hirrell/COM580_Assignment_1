using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_1
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var choice = -1;
      while (choice != 0)
      {
        Console.Write("Enter your choice (1-6, press 0 to exit): ");
        int.TryParse(Console.ReadLine(), out choice);
        switch (choice)
        {
          case 0:
            Console.WriteLine("Exiting...");
            return;
          case 1:
            ListOwners();
            break;
          case 2:
            ListPets();
            break;
          case 3:
            DisplayPracticeInfoAndVets();
            break;
          case 4:
            DisplayPetInfoAndVisits();
            break;
          case 5:
            ListPetsTreatedByVetOnDate();
            break;
          case 6:
            DisplayCostOfVisit();
            break;
          default:
            Console.WriteLine("Invalid choice.");
            break;
        }
      }
    }

    // 1) List the Names and contact details of all Owner who are customers of the veterinary practice, sorted on Surname.
    static void ListOwners()
    {
      using (var db = new VeterinaryPracticeModelContainer())
      {
        var vetRegNum = AskForVetRegNum();

        Console.WriteLine("|{0, -15}|{1, -15}|{2, -30}|{3, -15}|", "Firstname", "Lastname", "EmailAddress", "PhoneNumber");
        Console.WriteLine("|---------------|---------------|------------------------------|---------------|");
        var query = from o in db.Owners orderby o.Surname select o;
        foreach (var owner in query)
        {
          if (owner.Practice.RegNum == vetRegNum)
          {
            Console.WriteLine("|{0, -15}|{1, -15}|{2, -30}|{3, -15}|", owner.Firstname, owner.Surname, owner.EmailAddress, owner.PhoneNumber);
          }
        }
      }
    }

    // 2)	List the Pet Name, Type and Breed of all pets registered with the practice.
    static void ListPets()
    {
      using (var db = new VeterinaryPracticeModelContainer())
      {
        var vetRegNum = AskForVetRegNum();

        Console.WriteLine("|{0, -15}|{1, -15}|{2, -20}|", "Name", "Type", "Breed");
        Console.WriteLine("|---------------|---------------|--------------------|");
        var query = from p in db.Pets select p;
        foreach (var pet in query)
        {
          if (pet.Owner.PracticeRegNum == vetRegNum)
          {
            Console.WriteLine("|{0, -15}|{1, -15}|{2, -20}|", pet.Name, pet.Type, pet.Breed);
          }
        }
      }
    }

    // 3)	Given the RegNum(i.e.registration number for a vet practice) display the name and address of the practice and list the names of all vets working in the practice.
    static void DisplayPracticeInfoAndVets()
    {
      using (var db = new VeterinaryPracticeModelContainer())
      {
        var vetRegNum = AskForVetRegNum();

        var query = from p in db.Practices select p;
        foreach (var practice in query)
        {
          if (practice.RegNum == vetRegNum)
          {
            Console.WriteLine("Practice name: {0}", practice.Name);
            Console.WriteLine("Practice address: {0}", practice.Address);
            Console.WriteLine("Vets working in this practice:");
            foreach (var vet in practice.Vets)
            {
              Console.WriteLine("{0} {1}", vet.Firstname, vet.Surname);
            }
          }
        }
      }
    }

    // 4)	Given a PetId, list the Name, Type and Breed of the pet followed (in chronological order) by the date and reason for all visits by that pet to the veterinary practice.
    static void DisplayPetInfoAndVisits()
    {
      using (var db = new VeterinaryPracticeModelContainer())
      {
        var petId = 0;
        var query = from p in db.Pets select p;
        while (petId < 1 || petId > query.Count())
        {
          Console.Write("Enter the PetId of the pet that you want to view information about: ");
          int.TryParse(Console.ReadLine(), out petId);

          if (petId < 1 || petId > query.Count())
          {
            Console.WriteLine("Invalid pet Id");
          }
        }

        foreach (var pet in query)
        {
          if (pet.PetId == petId)
          {
            Console.WriteLine("Pet name: {0}", pet.Name);
            Console.WriteLine("Pet type: {0}", pet.Type);
            Console.WriteLine("Pet breed: {0}", pet.Breed);
            Console.WriteLine("Visits:");
            foreach (var visit in pet.Visits.OrderBy(v => v.Date))
            {
              Console.WriteLine("{0} - {1}", visit.Date, visit.Reason);
            }
          }
        }
      }
    }

    // 5)	Given a VetId and a specified date, for all pet appointments with that vet on that date, first list the vets name followed by the list of pets treated (i.e. the pet name) and also the pet owner’s name.
    static void ListPetsTreatedByVetOnDate()
    {
      using (var db = new VeterinaryPracticeModelContainer())
      {
        var vetId = 0;
        var query = from v in db.Vets select v;
        while (vetId < 1 || vetId > query.Count())
        {
          Console.Write("Enter the VetId of the vet that you want to view information about: ");
          int.TryParse(Console.ReadLine(), out vetId);

          if (vetId < 1 || vetId > query.Count())
          {
            Console.WriteLine("Invalid vet Id");
          }
        }

        var date = DateTime.MinValue;
        while (date == DateTime.MinValue)
        {
          Console.Write("Enter the date of the visit (dd/mm/yyyy): ");
          DateTime.TryParse(Console.ReadLine(), out date);

          if (date == DateTime.MinValue)
          {
            Console.WriteLine("Invalid date");
          }
        }
        var dateInDatabaseFormat = date.ToString("yyyy-MM-dd");

        foreach (var vet in query)
        {
          if (vet.VetId == vetId)
          {
            Console.WriteLine("Vet name: {0} {1}", vet.Firstname, vet.Surname);
            Console.WriteLine("Pets treated:");
            Console.WriteLine("|{0, -10}|{1, -20}|", "Name", "Owner");
            Console.WriteLine("|----------|--------------------|");
            foreach (var visit in vet.Visits)
            {
              if (visit.Date == dateInDatabaseFormat)
              {
                Console.WriteLine("|{0, -10}|{1, -20}|", visit.Pet.Name, visit.Pet.Owner.Firstname + " " + visit.Pet.Owner.Surname);
              }
            }
          }
        }
      }
    }

    // 6)	Given a PetNum calculate the cost of the most recent visit to the vet if the charge for an appointment is £40 and two or more medications (which are each an additional cost) have been given to the pet. The output should show the name of the pet and an itemised invoice.
    static void DisplayCostOfVisit()
    {
      using (var db = new VeterinaryPracticeModelContainer())
      {
        var petId = 0;
        var query = from p in db.Pets select p;
        while (petId < 1 || petId > query.Count())
        {
          Console.Write("Enter the PetId of the pet that you want to view information about: ");
          int.TryParse(Console.ReadLine(), out petId);

          if (petId < 1 || petId > query.Count())
          {
            Console.WriteLine("Invalid pet Id");
          }
        }

        foreach (var pet in query)
        {
          if (pet.PetId == petId)
          {
            Console.WriteLine("Pet name: {0}", pet.Name);
            Console.WriteLine("Most recent visit:");
            var visit = pet.Visits.OrderByDescending(v => v.Date).First();

            Console.WriteLine("{0} - {1}", visit.Date, visit.Reason);
            Console.WriteLine("|{0, -20}|{1, -10}|", "Item", "Cost");
            Console.WriteLine("|--------------------|----------|");
            Console.WriteLine("|{0, -20}|{1, -10}|", "Appointment", "£40");
            foreach (var medication in visit.Medications)
            {
              Console.WriteLine("|{0, -20}|{1, -10}|", medication.Name, "£" + medication.Cost);
            }
            Console.WriteLine("|--------------------|----------|");
            Console.WriteLine("|{0, -20}|{1, -10}|", "Total", "£" + (visit.Medications.Sum(m => int.Parse(m.Cost)) + 40));
          }
        }
      }
    }

    // helper functions
    static int AskForVetRegNum()
    {
      using (var db = new VeterinaryPracticeModelContainer())
      {
        var vetRegNum = 0;
        var query = from p in db.Practices select p;

        while (vetRegNum < 1 || vetRegNum > query.Count())
        {
          Console.Write("Enter the RegNum of the veterinary practice that you want to view information about: ");
          int.TryParse(Console.ReadLine(), out vetRegNum);

          if (vetRegNum < 1 || vetRegNum > query.Count())
          {
            Console.WriteLine("Invalid veterinary practice RegNum");
          }
        }
        return vetRegNum;
      }
    }
  }
}
