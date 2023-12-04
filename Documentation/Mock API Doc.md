## Summary
These are the endpoints of our Mock API

## Example Usage
```python
# Send a message
POST /send_message
{
    "sender_id": "653fad3009ae93a5292195d4",
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "message": "Hello, When do you have time to meet?",
    "sender_name": "Art Nooijen",
    "receiver_name": "jelle Manders"
}

# Send a ping
POST /send_ping
{
    "sender_id": "653fad3009ae93a5292195d4",
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "sender_name": "Art Nooijen",
    "receiver_name": "jelle Manders"
}

# Initiate a call
POST /send_call
{
    "sender_id": "653fad3009ae93a5292195d4",
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "sender_name": "Art Nooijen",
    "receiver_name": "jelle Manders"
}

# Check if a person is available at the current hour
POST /atm_available
{
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "receiver_name": "jelle Manders"
}

# Check if a person is available at a specific hour
POST /available
{
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "receiver_name": "jelle Manders",
    "request_time": "11"
}

# Set disturb status for a person
POST /disturb
{
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "receiver_name": "jelle Manders",
    "disturb": "true"
}

# Check if a person is on location or working from home
POST /is_on_location
{
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "receiver_name": "jelle Manders",
    "is_on_location": "true"
}
```

## Code Analysis
### Inputs
- `data`: The JSON data received in the request.
- `required_fields`: A list of fields that are required in the input data.
___
### Flow
1. The code defines a Flask application and sets up logging.
2. There is a function `validate_input` that checks if the required fields are present in the input data.
3. There are several routes defined for different actions like sending messages, pings, calls, and checking availability.
4. Each route validates the input data using the `validate_input` function and returns an error response if any required fields are missing.
5. The routes perform the required actions based on the input data and log the relevant information.
6. The routes return a JSON response with a status and message indicating the success or failure of the action.
___
### Outputs
- JSON response with a status and message indicating the success or failure of the action.
___
