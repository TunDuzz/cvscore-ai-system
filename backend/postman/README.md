# Postman Quick Start

1. Run the API:
   `dotnet run --project backend/CVScore.API`
2. Import:
   - `backend/postman/CVScore.API.postman_collection.json`
   - `backend/postman/CVScore.local.postman_environment.json`
3. Select the `CVScore Local` environment.
4. Run requests in order from `1` to `9`.

Notes:
- The environment uses the seeded demo IDs from `ApplicationDbSeeder`.
- On an existing database seeded before these fixed IDs were added, recreate the database or update the environment values manually.
- Request `2`, `5`, `6`, and `7` automatically capture IDs into collection variables for the next steps.
