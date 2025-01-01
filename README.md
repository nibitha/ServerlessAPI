# ServerlessAPI

The ToDoApi Controller contains the following http methods:
1. Create
Insert a new record into the database (DynamoDB or SQL).
HTTP Method: POST.
Input: JSON payload containing the details of the item to be created.
Output:
Return a success response with the ID of the created item (HTTP 201).
Handle errors for invalid input (HTTP 400) and database issues (HTTP 500).

2. Read
Retrieve data from the database.
HTTP Method: GET.
Scenarios:
1. Get All Records: Return all records if no query parameters are
provided.
2. Get by ID: Return a specific record when an ID is provided in the path
(/items/{id}).
3. Filter Records: Allow filtering using query parameters (e.g.,
?name=John&amp;status=active).
Output:
Success: Return matching data (HTTP 200).
Not Found: Return an empty response if no matching data is found(HTTP 204).
Handle errors for invalid input (HTTP 400) and database issues (HTTP500).


3. Update
Modify an existing record in the database.
HTTP Method: PUT or PATCH.
Input: Record ID in the path and updated data in the request body.
Output:
Success: Return the updated record or a confirmation message (HTTP200).
Not Found: Handle cases where the record does not exist (HTTP 404).
Handle validation errors (HTTP 400) and database issues (HTTP 500).

4. Delete
Remove a record from the database.
HTTP Method: DELETE.
Input: Record ID in the path.
Output:
Success: Return a confirmation message (HTTP 200).
Not Found: Handle cases where the record does not exist (HTTP 404).
Handle database issues (HTTP 500).
