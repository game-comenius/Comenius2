public enum CharacterName
{
    Jean,
    Vladmir,
    Paulino,
    Celestino,
    Montanari,
    Antonia,
    Alice,
}

public static class CharacterNameExtensions
{
    public static string NomeCompleto(this CharacterName characterName)
    {
        switch (characterName)
        {
            case CharacterName.Jean:
                return "Jean Pires Filho";
            case CharacterName.Vladmir:
                return "Vladmir Mikovitch";
            case CharacterName.Paulino:
                return "Paulino do Rego Freitas";
            case CharacterName.Celestino:
                return "Celestino";
            case CharacterName.Montanari:
                return "Maria Montanari";
            case CharacterName.Antonia:
                return "Antônia";
            case CharacterName.Alice:
                return "Alice Menezes";
            default:
                return "";
        }
    }
}