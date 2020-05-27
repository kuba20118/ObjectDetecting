using System;
using System.Collections.Generic;
using System.Linq;

namespace Detector.Core.Domain
{

  public class Statistics
  {
    public Guid Id { get; protected set; }
    public Guid ImageId { get; protected set; }
    public List<string> FoundObjects { get; protected set; }
    public Feedback FeedbackFromUser { get; protected set; }
    public int NumberOfObjectsFound { get; protected set; }
    public long Time { get; protected set; }
    public int CritMistakes { get; protected set; }
    public int AllMistakes { get; protected set; }


    public Statistics(Guid imageId, List<string> description, long time, Feedback feedback)
    {
      FoundObjects = new List<string>();
      Id = Guid.NewGuid();
      Time = time;
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
      localObjCounter += feedback.IncorrectBox;

      if (feedback.NotFound < 0)
        throw new DomainException(ErrorCodes.InvalidFeedbackData, "Niepoprawne dane");

      if (feedback.NotFound > 5)
        feedback.NotFound = 5;

      if (feedback.Correct < 0)
        throw new DomainException(ErrorCodes.InvalidFeedbackData, "Niepoprawne dane");

      localObjCounter += feedback.Correct;

      if (localObjCounter != NumberOfObjectsFound)
        throw new DomainException(ErrorCodes.InvalidFeedbackData, "Niepoprawne dane");

      FeedbackFromUser = feedback;
      CritMistakes += (feedback.Incorrect + feedback.NotFound);
      AllMistakes += (CritMistakes + feedback.IncorrectBox + feedback.MultipleFound);
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
        FoundObjects.Add(key);
      }
      NumberOfObjectsFound = description.Count;
    }
  }
}