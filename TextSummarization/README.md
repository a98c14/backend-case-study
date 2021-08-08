# Text Summarizer

Generates a summary from input text. Summary sentence length can be specified when running the program

## Example

To generate a summary with 3 sentences

```
dotnet run "./sample.txt" 3 
```

 ## Build

 To generate an self-contained .exe for win64, below command can be used

 ```
 dotnet publish .\TextSummarization.csproj -o .\app -r win-x64 --self-contained=true /p:PublishSingleFile=true
 ```