
public class ItemDescriptionsInOneMission {

    public string StandardDescription;

    public string FirstMomentDescription;
    public string SecondMomentDescription;
    public string ThirdMomentDescription;

    //diálogo da lurdinha ao pegar a mídia
    public string DialogueWhenAcquired;

    //descrição no inventário
    public string InventoryDescription;

    //descrições do "Saiba Mais"
    public string Geral;
    public string Com;
    public string Sobre;
    public string Atraves;

    public ItemDescriptionsInOneMission()
    {
        StandardDescription = "";

        FirstMomentDescription = "";
        SecondMomentDescription = "";
        ThirdMomentDescription = "";

        DialogueWhenAcquired = "";
        InventoryDescription = "";
        Geral = "";
        Com = "";
        Sobre = "";
        Atraves = "";
    }
}
