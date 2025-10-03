# Todo API - .NET 8 Minimal API

Exemple d'API REST construite avec **.NET 8** et **Minimal APIs**, utilisant une architecture légère et modulaire. Cette API permet de gérer une liste de tâches (CRUD) avec deux champs : ***titre*** et ***terminé ?***. Elle est conçue pour fonctionner avec une base de données **PostgreSQL** via Docker, et intègre **Swagger** pour une documentation interactive.
---
## 🏗️ Architecture

- **Minimal APIs** : Utilisation des Minimal APIs de .NET 8 pour une syntaxe concise et performante, sans contrôleurs.
- **Base de données** : PostgreSQL en conteneur Docker, configurée via `docker-compose.yml`.
- **Entity Framework Core** : Pour l’accès aux données et les migrations.
- **Swagger** : Documentation interactive de l’API, accessible à l’URL `/swagger`.

---

## 🚀 Lancer le projet

### 1. **Sans Docker (local)**
```bash
dotnet restore
dotnet build
dotnet run --project src/TodoApi
```
L’API sera accessible à l’adresse : `https://localhost:5241` (ou `http://localhost:5000` selon la configuration).

### 2. **Avec Docker (recommandé)**
Lance les conteneurs PostgreSQL et l’application avec :
```bash
docker-compose up --build
```
* L’API sera accessible à : `http://localhost:5000`
* La base de données PostgreSQL sera disponible sur le port 5432 (en interne pour les conteneurs).


> "**Note :** La première exécution peut prendre quelques minutes pour télécharger les images Docker et configurer la base de données."
---
## 📚 Documentation avec Swagger
Une fois l’API lancée, accède à la documentation interactive Swagger :

* **URL :** `http://localhost:5000/swagger` (ou `https://localhost:5241/swagger` si tu utilises HTTPS en local).
* **Fonctionnalités :**
    * Liste de tous les endpoints disponibles.
    * Possibilité de tester directement les requêtes depuis l’interface.
    * Description des modèles de données (ex : `TodoItem`).

> **Astuce :** Swagger est activé par défaut en environnement de développement. Pour le désactiver, retire `builder.Services.AddSwaggerGen();` dans `Program.cs`.
---
## 🔧 Configuration
* Les paramètres de connexion à la base de données sont définis dans le `docker-compose.yml` et dans `Program.cs` via builder.`Configuration.GetConnectionString("DefaultConnection")`.
* Pour appliquer les migrations Entity Framework Core (si nécessaire) :
```bash
dotnet ef database update --project src/TodoApi
```
---
## 📌 Exemples d'utilisation (avec curl)

### Créer une tâche
```bash
curl -X POST https://localhost:5241/todos \
  -H "Content-Type: application/json" \
  -d '{"title":"Apprendre .NET 8","isDone":false}'
```

### Lister les tâches
```bash
curl https://localhost:5241/todos
```

### Récupérer une tâche par ID
```bash
curl https://localhost:5241/todos/{id}
```
*Remplace `{id}` par l’identifiant de la tâche.*

### Mettre à jour une tâche
```bash
curl -X PUT https://localhost:5241/todos/{id} \
  -H "Content-Type: application/json" \
  -d '{"title":"Nouveau titre","isDone":true}'
```
*Remplace `{id}` par l’identifiant de la tâche.*

### Supprimer une tâche
```bash
curl -X DELETE https://localhost:5241/todos/{id}
```
*Remplace `{id}` par l’identifiant de la tâche.*




