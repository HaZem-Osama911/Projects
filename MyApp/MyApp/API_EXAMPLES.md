# API Examples - Quick Reference Guide

This document provides practical examples for using the MyApp API endpoints.

## Base URL
- **Development**: `https://localhost:7085/api`
- **HTTP**: `http://localhost:5169/api`

---

## Movies API Examples

### 1. Get All Movies

**Request:**
```http
GET /api/Movies HTTP/1.1
Host: localhost:7085
```

**Response:**
```json
HTTP/1.1 200 OK
Content-Type: application/json

[
  {
    "id": 1,
    "title": "The Matrix",
    "year": 1999,
    "rate": 8.7,
    "storeLuine": "A computer hacker learns about the true nature of reality...",
    "poster": "https://example.com/matrix-poster.jpg",
    "genreId": 1,
    "genre": {
      "id": 1,
      "name": "Sci-Fi"
    }
  },
  {
    "id": 2,
    "title": "Inception",
    "year": 2010,
    "rate": 8.8,
    "storeLuine": "A thief who steals corporate secrets through dream-sharing...",
    "poster": "https://example.com/inception-poster.jpg",
    "genreId": 1,
    "genre": {
      "id": 1,
      "name": "Sci-Fi"
    }
  }
]
```

**cURL:**
```bash
curl -X GET "https://localhost:7085/api/Movies" \
  -H "Accept: application/json"
```

**JavaScript (Fetch):**
```javascript
fetch('https://localhost:7085/api/Movies')
  .then(response => response.json())
  .then(data => console.log(data))
  .catch(error => console.error('Error:', error));
```

**C# (HttpClient):**
```csharp
using var client = new HttpClient();
var response = await client.GetAsync("https://localhost:7085/api/Movies");
var movies = await response.Content.ReadFromJsonAsync<List<Movie>>();
```

---

### 2. Get Movie by ID

**Request:**
```http
GET /api/Movies/1 HTTP/1.1
Host: localhost:7085
```

**Response (Success):**
```json
HTTP/1.1 200 OK
Content-Type: application/json

{
  "id": 1,
  "title": "The Matrix",
  "year": 1999,
  "rate": 8.7,
  "storeLuine": "A computer hacker learns about the true nature of reality...",
  "poster": "https://example.com/matrix-poster.jpg",
  "genreId": 1,
  "genre": {
    "id": 1,
    "name": "Sci-Fi"
  }
}
```

**Response (Not Found):**
```json
HTTP/1.1 404 Not Found
Content-Type: application/json

{
  "message": "Movie with ID 999 not found."
}
```

**cURL:**
```bash
curl -X GET "https://localhost:7085/api/Movies/1" \
  -H "Accept: application/json"
```

**JavaScript:**
```javascript
fetch('https://localhost:7085/api/Movies/1')
  .then(response => {
    if (response.ok) {
      return response.json();
    }
    throw new Error('Movie not found');
  })
  .then(movie => console.log(movie))
  .catch(error => console.error('Error:', error));
```

---

### 3. Create Movie

**Request:**
```http
POST /api/Movies HTTP/1.1
Host: localhost:7085
Content-Type: application/json

{
  "title": "Interstellar",
  "year": 2014,
  "rate": 8.6,
  "storeLuine": "A team of explorers travel through a wormhole in space...",
  "poster": "https://example.com/interstellar-poster.jpg",
  "genreId": 1
}
```

**Response:**
```json
HTTP/1.1 201 Created
Content-Type: application/json
Location: https://localhost:7085/api/Movies/3

{
  "id": 3,
  "title": "Interstellar",
  "year": 2014,
  "rate": 8.6,
  "storeLuine": "A team of explorers travel through a wormhole in space...",
  "poster": "https://example.com/interstellar-poster.jpg",
  "genreId": 1,
  "genre": null
}
```

**cURL:**
```bash
curl -X POST "https://localhost:7085/api/Movies" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{
    "title": "Interstellar",
    "year": 2014,
    "rate": 8.6,
    "storeLuine": "A team of explorers travel through a wormhole in space...",
    "poster": "https://example.com/interstellar-poster.jpg",
    "genreId": 1
  }'
```

**JavaScript:**
```javascript
const newMovie = {
  title: "Interstellar",
  year: 2014,
  rate: 8.6,
  storeLuine: "A team of explorers travel through a wormhole in space...",
  poster: "https://example.com/interstellar-poster.jpg",
  genreId: 1
};

fetch('https://localhost:7085/api/Movies', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify(newMovie)
})
  .then(response => response.json())
  .then(data => console.log('Created:', data))
  .catch(error => console.error('Error:', error));
```

**C#:**
```csharp
var movie = new MovieDto
{
    Title = "Interstellar",
    Year = 2014,
    Rate = 8.6,
    StoreLuine = "A team of explorers travel through a wormhole in space...",
    Poster = "https://example.com/interstellar-poster.jpg",
    GenreId = 1
};

var json = JsonSerializer.Serialize(movie);
var content = new StringContent(json, Encoding.UTF8, "application/json");
var response = await client.PostAsync("https://localhost:7085/api/Movies", content);
var createdMovie = await response.Content.ReadFromJsonAsync<Movie>();
```

---

### 4. Update Movie

**Request:**
```http
PUT /api/Movies/3 HTTP/1.1
Host: localhost:7085
Content-Type: application/json

{
  "title": "Interstellar (Updated)",
  "year": 2014,
  "rate": 8.7,
  "storeLuine": "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
  "poster": "https://example.com/interstellar-poster.jpg",
  "genreId": 1
}
```

**Response (Success):**
```json
HTTP/1.1 200 OK
Content-Type: application/json

{
  "id": 3,
  "title": "Interstellar (Updated)",
  "year": 2014,
  "rate": 8.7,
  "storeLuine": "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
  "poster": "https://example.com/interstellar-poster.jpg",
  "genreId": 1,
  "genre": null
}
```

**cURL:**
```bash
curl -X PUT "https://localhost:7085/api/Movies/3" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{
    "title": "Interstellar (Updated)",
    "year": 2014,
    "rate": 8.7,
    "storeLuine": "Updated storyline...",
    "poster": "https://example.com/interstellar-poster.jpg",
    "genreId": 1
  }'
```

**JavaScript:**
```javascript
const updatedMovie = {
  title: "Interstellar (Updated)",
  year: 2014,
  rate: 8.7,
  storeLuine: "Updated storyline...",
  poster: "https://example.com/interstellar-poster.jpg",
  genreId: 1
};

fetch('https://localhost:7085/api/Movies/3', {
  method: 'PUT',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify(updatedMovie)
})
  .then(response => response.json())
  .then(data => console.log('Updated:', data))
  .catch(error => console.error('Error:', error));
```

---

### 5. Delete Movie

**Request:**
```http
DELETE /api/Movies/3 HTTP/1.1
Host: localhost:7085
```

**Response (Success):**
```json
HTTP/1.1 200 OK
Content-Type: application/json

{
  "message": "Movie Deleted Successfully"
}
```

**Response (Not Found):**
```json
HTTP/1.1 404 Not Found
Content-Type: application/json

{
  "message": "Movie with ID 999 not found."
}
```

**cURL:**
```bash
curl -X DELETE "https://localhost:7085/api/Movies/3" \
  -H "Accept: application/json"
```

**JavaScript:**
```javascript
fetch('https://localhost:7085/api/Movies/3', {
  method: 'DELETE'
})
  .then(response => response.json())
  .then(data => console.log('Deleted:', data))
  .catch(error => console.error('Error:', error));
```

---

## Genres API Examples

### 1. Get All Genres

**Request:**
```http
GET /api/Genres HTTP/1.1
Host: localhost:7085
```

**Response:**
```json
HTTP/1.1 200 OK
Content-Type: application/json

[
  {
    "id": 1,
    "name": "Sci-Fi"
  },
  {
    "id": 2,
    "name": "Action"
  },
  {
    "id": 3,
    "name": "Comedy"
  }
]
```

**cURL:**
```bash
curl -X GET "https://localhost:7085/api/Genres" \
  -H "Accept: application/json"
```

---

### 2. Get Genre by ID

**Request:**
```http
GET /api/Genres/1 HTTP/1.1
Host: localhost:7085
```

**Response:**
```json
HTTP/1.1 200 OK
Content-Type: application/json

{
  "id": 1,
  "name": "Sci-Fi"
}
```

---

### 3. Create Genre

**Request:**
```http
POST /api/Genres HTTP/1.1
Host: localhost:7085
Content-Type: application/json

{
  "name": "Thriller"
}
```

**Response:**
```json
HTTP/1.1 201 Created
Content-Type: application/json
Location: https://localhost:7085/api/Genres/4

{
  "id": 4,
  "name": "Thriller"
}
```

**cURL:**
```bash
curl -X POST "https://localhost:7085/api/Genres" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{
    "name": "Thriller"
  }'
```

---

### 4. Update Genre

**Request:**
```http
PUT /api/Genres/4 HTTP/1.1
Host: localhost:7085
Content-Type: application/json

{
  "name": "Psychological Thriller"
}
```

**Response:**
```json
HTTP/1.1 200 OK
Content-Type: application/json

{
  "id": 4,
  "name": "Psychological Thriller"
}
```

---

### 5. Delete Genre

**Request:**
```http
DELETE /api/Genres/4 HTTP/1.1
Host: localhost:7085
```

**Response:**
```json
HTTP/1.1 200 OK
Content-Type: application/json

{
  "message": "Genre Deleted Successfully"
}
```

---

## Error Handling

### Common HTTP Status Codes

- **200 OK** - Request successful
- **201 Created** - Resource created successfully
- **400 Bad Request** - Invalid request data (validation errors)
- **404 Not Found** - Resource not found
- **500 Internal Server Error** - Server error

### Error Response Format

**Validation Error (400):**
```json
{
  "title": ["The Title field is required."],
  "year": ["The Year field must be a valid number."]
}
```

**Not Found (404):**
```json
{
  "message": "Movie with ID 999 not found."
}
```

---

## Testing with Postman

### Import Collection

1. Open Postman
2. Create a new Collection: "MyApp API"
3. Add requests for each endpoint

### Environment Variables

Create a Postman Environment with:
- `baseUrl`: `https://localhost:7085`
- `apiBase`: `{{baseUrl}}/api`

### Example Postman Request

**Request Name:** Create Movie
**Method:** POST
**URL:** `{{apiBase}}/Movies`
**Headers:**
- `Content-Type`: `application/json`
- `Accept`: `application/json`

**Body (raw JSON):**
```json
{
  "title": "Test Movie",
  "year": 2024,
  "rate": 7.5,
  "storeLuine": "Test storyline",
  "poster": "test-poster.jpg",
  "genreId": 1
}
```

---

## Testing with Swagger UI

1. Run the application
2. Navigate to: `https://localhost:7085/swagger`
3. Use the interactive API documentation
4. Click "Try it out" on any endpoint
5. Fill in parameters and click "Execute"

---

## Rate Limiting & Best Practices

### Current Status
- ⚠️ No rate limiting implemented
- ⚠️ No authentication required for API endpoints

### Recommendations
1. Implement JWT authentication
2. Add rate limiting middleware
3. Use HTTPS in production
4. Validate all input data
5. Implement pagination for list endpoints
6. Add request/response logging

---

## Pagination Example (Future Enhancement)

**Request:**
```http
GET /api/Movies?page=1&pageSize=10 HTTP/1.1
```

**Response:**
```json
{
  "data": [...],
  "page": 1,
  "pageSize": 10,
  "totalCount": 100,
  "totalPages": 10
}
```

---

## Filtering Example (Future Enhancement)

**Request:**
```http
GET /api/Movies?genreId=1&minRate=8.0 HTTP/1.1
```

**Response:**
```json
[
  {
    "id": 1,
    "title": "The Matrix",
    "rate": 8.7,
    ...
  }
]
```

---

## Notes

- All endpoints return JSON
- Dates are in ISO 8601 format
- All IDs are integers
- GenreId must reference an existing Genre
- Title max length: 250 characters
- StoreLuine (Storyline) max length: 2500 characters
- Genre Name max length: 100 characters

---

**Last Updated:** 2024

