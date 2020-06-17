public enum CharacterName
{
    Jean,
    Vladmir,
    Paulino,
    Celestino,
    Montanari,
    Antonia,
    Alice,
    Diretor,
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
            case CharacterName.Diretor:
                return "João Frederico";
            default:
                return "";
        }
    }

    public static string PrimeiroNome(this CharacterName characterName)
    {
        switch (characterName)
        {
            case CharacterName.Jean:
                return "Jean";
            case CharacterName.Vladmir:
                return "Vladmir";
            case CharacterName.Paulino:
                return "Paulino";
            case CharacterName.Celestino:
                return "Celestino";
            case CharacterName.Montanari:
                return "Maria";
            case CharacterName.Antonia:
                return "Antônia";
            case CharacterName.Alice:
                return "Alice";
            case CharacterName.Diretor:
                return "João";
            default:
                return "";
        }
    }
}