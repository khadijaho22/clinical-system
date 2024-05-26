using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PhysiotherapyExercise
{
    public string Name { get; set; }
    public bool RequiresSpecialAttention { get; set; }
    public int DurationInMinutes { get; set; }
    public string TargetArea { get; set; }
    public string EquipmentRequired { get; set; }
    public string Instructions { get; set; }

    public PhysiotherapyExercise(string name, bool requiresSpecialAttention, int durationInMinutes, string targetArea, string equipmentRequired, string instructions)
    {
        Name = name;
        RequiresSpecialAttention = requiresSpecialAttention;
        DurationInMinutes = durationInMinutes;
        TargetArea = targetArea;
        EquipmentRequired = equipmentRequired;
        Instructions = instructions;
    }

    public void DisplayExerciseDetails()
    {
        Console.WriteLine($"Exercise Details - Name: {Name}, Duration: {DurationInMinutes} minutes, Target Area: {TargetArea}, Equipment: {EquipmentRequired}, Instructions: {Instructions}");
    }
}

class Patient
{
    public bool HasInjury { get; set; }
    public int Age { get; set; }
    public string MedicalHistory { get; set; }
    public bool IsPregnant { get; set; }
    public bool IsAthlete { get; set; }

    public Patient(bool hasInjury, int age, string medicalHistory, bool isPregnant, bool isAthlete)
    {
        HasInjury = hasInjury;
        Age = age;
        MedicalHistory = medicalHistory;
        IsPregnant = isPregnant;
        IsAthlete = isAthlete;
    }

    public void DisplayPatientDetails()
    {
        Console.WriteLine($"Patient Details - Age: {Age}, Injury: {(HasInjury ? "Yes" : "No")}, Medical History: {MedicalHistory}, Pregnant: {IsPregnant}, Athlete: {IsAthlete}");
    }
}

class DecisionSupportSystem
{
    private List<PhysiotherapyExercise> highRiskExercises;

    public DecisionSupportSystem()
    {
        highRiskExercises = new List<PhysiotherapyExercise>
        {
            new PhysiotherapyExercise("ExerciseA", true, 20, "Lower Back", "Resistance Bands", "Follow provided instructions"),
            new PhysiotherapyExercise("ExerciseB", true, 15, "Knees", "None", "Be cautious and perform under supervision"),
            // Add more high-risk exercises
        };
    }

    public bool IsExerciseSafeForPatient(PhysiotherapyExercise exercise, Patient patient)
    {
        if (exercise.RequiresSpecialAttention && patient.IsPregnant)
        {
            Console.WriteLine($"Exercise {exercise.Name} requires special attention and may not be safe for pregnant patients.");
            return false;
        }

        // Add more conditions based on your requirements

        if (highRiskExercises.Contains(exercise) && patient.HasInjury)
        {
            Console.WriteLine($"Exercise {exercise.Name} is not safe for the patient.");
            return false;
        }

        Console.WriteLine($"Exercise {exercise.Name} is safe for the patient.");
        return true;
    }

    // Add the following methods:

    public string GetTreatmentPlan(string selectedBodyPart, string selectedInjury)
    {
        // Implement your logic to determine the treatment plan based on the selected body part and injury
        // Return the treatment plan as a string
        return "Sample Treatment Plan";
    }

    public List<string> GetTreatmentOptions(string comboBoxName, string treatmentPlan)
    {
        // Implement your logic to determine the treatment options based on the combo box name and treatment plan
        // Return the treatment options as a list of strings
        return new List<string> { "Option1", "Option2", "Option3" };
    }
}
