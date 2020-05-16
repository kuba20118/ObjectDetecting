using System;
using System.Collections.Generic;
using System.Linq;

namespace Detector.Core.Domain
{

    public class Statistics
    {
        public Guid Id { get; protected set; }
        public Guid ImageId { get; protected set; }
        public List<Tuple<string, int>> FoundObjects { get; protected set; }
        public Feedback FeedbackFromUser { get; protected set; }
        public int NumberOfObjectsFound { get; protected set; }
        public bool Valid { get; protected set; }


        public Statistics(Guid imageId, List<string> description, Feedback feedback)
        {
            FoundObjects = new List<Tuple<string, int>>();
            Id = Guid.NewGuid();
            SetImageId(imageId);
            SetFoundObjects(description);
            SetFeedback(feedback);
        }

        private void SetImageId(Guid imageId)
        {
            if (imageId.ToString().Length == 0)
                throw new DomainException(ErrorCodes.InvalidGuid, "Niepoprawny guid");

            if (ImageId == imageId)
                return;

            ImageId = imageId;
        }

        private void SetFeedback(Feedback feedback)
        {
            int localObjCounter = 0;
            if (feedback == null)
                throw new DomainException(ErrorCodes.InvalidFeedback, "Niepoprawny feedback");

            if (feedback.Incorrect < 0)
                throw new DomainException(ErrorCodes.InvalidFeedbackData, "Niepoprawne dane");
            localObjCounter += feedback.Incorrect;

            if (feedback.MultipleFound < 0)
                throw new DomainException(ErrorCodes.InvalidFeedbackData, "Niepoprawne dane");
            localObjCounter += feedback.MultipleFound;

            if (feedback.IncorrectBox < 0)
                throw new DomainException(ErrorCodes.InvalidFeedbackData, "Niepoprawne dane");

            if (feedback.NotFound < 0)
                throw new DomainException(ErrorCodes.InvalidFeedbackData, "Niepoprawne dane");

            if (feedback.Correct < 0)
                throw new DomainException(ErrorCodes.InvalidFeedbackData, "Niepoprawne dane");

            localObjCounter += feedback.Correct;

            if (localObjCounter != NumberOfObjectsFound)
                throw new DomainException(ErrorCodes.InvalidFeedbackData, "Niepoprawne dane");
            
            FeedbackFromUser = feedback;
        }

        private void SetFoundObjects(List<string> description)
        {
            if (description.Count == 0 || description == null)
            {
                NumberOfObjectsFound = 0;
                FoundObjects = null;
                return;
            }

            foreach (var obj in description)
            {
                var key = obj.Split('(').First();
                var content = obj.Split('(', '%')[1];

                int value;
                if (!Int32.TryParse(content, out value))
                    throw new DomainException(ErrorCodes.InvalidDataFoundByML, "Nieprawidłowe dane");


                if (value < 0)
                    throw new DomainException(ErrorCodes.InvalidDataFoundByML, "Nieprawidłowe dane");


                FoundObjects.Add(new Tuple<string, int>(key, value));
            }

            NumberOfObjectsFound = description.Count;
        }
    }
}