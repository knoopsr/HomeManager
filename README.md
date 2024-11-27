# Naming Conventions & Afspraken
## Taalgebruik
- Engels
- Nederlands

## Indentation
- Allman Indentation

![Indentation](/readme/1.png)

## Naming Convention
### WPF Controls
- [Control in Hungarian notation] -> [generalized word] -> [specific word]

![Notation](/readme/2.png)
![ControlsList](/readme/3.png)

### MVVM Concepts
| Kind | Rule |
|---|---|
| ViewModel | PascalCaseViewModel |
| Model | PascalCaseModel |
| View | PascalCaseView |
| Repositories | IPascalCaseRepository |
| Services | IPascalCaseService |

### C#
| Kind | Rule |
|---|---|
| Field | _camelCase |
| Property  | PascalCase |
| Method    | PascalCase |
| Class | clsPascalCase |
| Interface | IPascalCase |
| Local variable | camelCase |
| Parameter | camelCase |

![Csharp](/readme/4.png)

## SQL
### Stored Procedure Convention
- [schema].[I/U/D/...][_PascalCase]

| Kind | Rule |
|---|---|
| Stored Procedure | dbo.D_PascalCase |

## Documentation
### XML
1. Maak gebruik van de ingebouwde [XML-documentatie](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/)
2. XML-documentatie > single line of multi-line comments
3. Maak gebruik van de [class diagram](https://stackoverflow.com/questions/17191218/generate-a-class-diagram-from-visual-studio) file

![XML](/readme/5.png)

Als ik nu een instantie wil maken van deze class in een andere file dan zie ik deze comment.

![XmlExample](/readme/6.png)

De xml documentatie bevat nog een aantal handige [tags](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags) zoals `<code>` of `<para>`of `<see>`.

![XmlExample2](/readme/7.png)
![XmlExample3](/readme/8.png)

### XML Diagram files
> [!IMPORTANT] 
> Installeer de Class Diagram component eerst met de Visual Studio Installer.

1. Add new .cd (class diagram) file
2. Sleep de namespace of class erin waarvan je een diagram wilt.
3. Nu kan je dit nog opmaken.

![ClassDiagram](/readme/9.png)
