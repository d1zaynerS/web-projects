using WebApplication11.Models;

namespace WebApplication11.Services
{
    public class SantaService
    {
        private readonly List<string> _participants = new List<string>
        {
            "Олександр", "Марія", "Іван", "Анна", "Дмитро"
        };

        public List<SantaPair> DrawSantas()
        {
            var givers = new List<string>(_participants);
            var receivers = new List<string>(_participants);
            var rand = new Random();
            var result = new List<SantaPair>();

            bool valid = false;
            while (!valid)
            {
                receivers = receivers.OrderBy(_ => rand.Next()).ToList();
                valid = true;
                for (int i = 0; i < givers.Count; i++)
                {
                    if (givers[i] == receivers[i])
                    {
                        valid = false;
                        break;
                    }
                }
            }

            for (int i = 0; i < givers.Count; i++)
            {
                result.Add(new SantaPair { Giver = givers[i], Receiver = receivers[i] });
            }

            return result;
        }
    }
}