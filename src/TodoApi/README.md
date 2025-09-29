# Todo API - .NET 8 Minimal API

Exemple d'API REST construite avec **.NET 8** et **Minimal APIs**.  
Elle permet de gérer une liste de tâches (CRUD).

## 🧪 Exemples d'utilisation (avec curl)

### Créer une tâche
```bash
curl -X POST https://<ton-codespaces-id>-5241.app.github.dev/todos \
  -H "Content-Type: application/json" \
  -d '{"title":"Apprendre .NET 8","isDone":false}'

### Lister les tâches
```bash
curl https://<ton-codespaces-id>-5241.app.github.dev/todos


## 🚀 Lancer le projet
```bash
dotnet restore
dotnet build
dotnet run --project src/TodoApi
