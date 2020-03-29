using System;
using CodingCampusCSharpHomework;

namespace HomeworkTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<Task3, string> TaskSolver = task =>
            {
                // Your solution goes here
                // You can get all needed inputs from task.[Property]
                // Good luck!
                string UserLongitude = task.UserLongitude;
                string UserLatitude = task.UserLatitude;
                int placesAmount = task.DefibliratorStorages.Length;

                bool isParseable = float.TryParse(UserLongitude.Replace(',', '.'), out float userLongitude);
                isParseable &= float.TryParse(UserLatitude.Replace(',', '.'), out float userLatitude);
                if (!isParseable)
                {
                    return "Data error: User's Lattitude or Longitude can't be parsed";
                }

                float minDistance = float.MaxValue;
                string result = "";

                for (int i = 0; i < placesAmount; i++)
                {
                    string defibliratorStorage = task.DefibliratorStorages[i];
                    string[] defibDescription = defibliratorStorage.Split(';');
                    if (defibDescription.Length != 4)
                    {
                        return $"Data error {i}: Defiblirator desscription differs from expected";
                    }
                    
                    string defibliratorName = defibDescription[0];
                    string defibliratorAddress = defibDescription[1];

                    isParseable &= float.TryParse(defibDescription[2].Replace(',', '.'),
                        out float defibliratorLongitude);
                    isParseable &= float.TryParse(defibDescription[3].Replace(',', '.'),
                        out float defibliratorLatitude);

                    if (!isParseable)
                    {
                        return $"Data error {i}: Lattitude or Longitude can't be parsed";
                    }

                    float distance = CalculateDistance(defibliratorLongitude,
                        defibliratorLatitude,
                        userLongitude,
                        userLatitude);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        result = $"Name: {defibliratorName}; Address: {defibliratorAddress}";
                    }
                }

                return result;
            };

            Task3.CheckSolver(TaskSolver);
        }

        static private float CalculateDistance(float defibLong, float defibLat, float userLong, float userLat)
        {
            const int radiansCoef = 6371;
            float x = (defibLong - userLong) * MathF.Cos((userLat + defibLat) / 2.0f);
            float y = (defibLat - userLat);
            return MathF.Sqrt(x * x + y * y) * radiansCoef;
        }
    }
}
