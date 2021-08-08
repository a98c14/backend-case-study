## Receipt Parser

Groups sections given by OCR service line by line. To do this, all sections are grouped by their y coordinate and ordered by their x coordinate. Writes the output to "output.txt"

To run:

```
dotnet run "./response.json"
```

Output path can also be specified:
```
dotnet run "./response.json" "myoutput.txt"
```
