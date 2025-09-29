# Todo API - .NET 8 Minimal API

Exemple d'API REST construite avec **.NET 8** et **Minimal APIs**.  
Elle permet de gérer une liste de tâches (CRUD).

## Exemples d'utilisation (avec curl)

### Créer une tâche
```bash
curl -X POST https://localhost:5241/todos \
  -H "Content-Type: application/json" \
  -d '{"title":"Apprendre .NET 8","isDone":false}'

### Lister les tâches
curl https://localhost:5241/todos


## 🚀 Lancer le projet
dotnet restore
dotnet build
dotnet run --project src/TodoApi
