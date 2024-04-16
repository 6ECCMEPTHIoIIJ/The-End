using AB_Utility.FromSceneToEntityConverter;

namespace Client.Components
{
    public class EcsActionInputConverter : ComponentConverter<EcsActionInput>
    {
        
    }

    public struct EcsActionInput
    {
        public bool MainActionPerformed { get; set; }
        public bool SecondaryActionPerformed { get; set; }
    }
}