from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/send_message', methods=['POST'])
def send_message():
    data = request.json
    sender_id = data.get('sender_id')
    receiver_id = data.get('receiver_id')
    message = data.get('message')
    sender_name = data.get('sender_name')
    receiver_name = data.get('receiver_name')

    # {
    # "sender_name": "Art",
    # "reciever_name": "jelle",
    # "sender_id": "1",
    # "receiver_id": "2",
    # "message": "Hello, When do you have time to meet?"
    # }
    print(f"Message from {sender_name}({sender_id}) to {receiver_name}({receiver_id}): {message}")

    return jsonify({"status": "success", "message": "Message sent successfully"})

@app.route('/send_ping', methods=['POST'])
def Send_ping():
    data = request.json
    sender_id = data.get('sender_id')
    receiver_id = data.get('receiver_id')
    message = 'is looking for you!'
    sender_name = data.get('sender_name')
    receiver_name = data.get('receiver_name')


    print(f"{sender_name}({sender_id}) {message} Send to: {receiver_name}({receiver_id})")

    return jsonify({"status": "success", "message": "User pinged successfully"})


if __name__ == '__main__':
    app.run(debug=True, port=8001)