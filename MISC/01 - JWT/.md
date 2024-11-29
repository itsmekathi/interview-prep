### JSON Web Token (JWT)

**JSON Web Token (JWT)** is an open standard (RFC 7519) that defines a compact and self-contained way for securely transmitting information between parties as a JSON object. The information can be verified and trusted because it is digitally signed. JWTs are commonly used for authentication and information exchange in web applications.

#### **Structure of a JWT**

A JWT is composed of three parts, separated by dots (`.`):

1. **Header**:

   - The header typically consists of two parts: the type of the token (JWT) and the signing algorithm being used (e.g., HMAC SHA256 or RSA).
   - Example:
     ```json
     {
       "alg": "HS256",
       "typ": "JWT"
     }
     ```

2. **Payload**:

   - The payload contains the claims, which are statements about an entity (typically, the user) and additional data. Claims can be registered (standardized), public, or private.
   - Registered claims include:
     - `sub`: Subject (the user).
     - `iat`: Issued at (timestamp when the token was issued).
     - `exp`: Expiration time (timestamp when the token expires).
   - Example:
     ```json
     {
       "sub": "1234567890",
       "name": "John Doe",
       "admin": true,
       "iat": 1516239022,
       "exp": 1516242622
     }
     ```

3. **Signature**:
   - To create the signature part, you take the encoded header, the encoded payload, a secret, and the algorithm specified in the header. This is done to verify that the sender of the JWT is who it claims to be and to ensure that the message wasn't changed along the way.
   - Example using HMAC SHA256:
     ```plaintext
     HMACSHA256(
       base64UrlEncode(header) + "." +
       base64UrlEncode(payload),
       your-256-bit-secret
     )
     ```

The final JWT looks like this:

```plaintext
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

#### **How JWT Works in Authentication**

1. **User Authentication**: The user logs in with their credentials (username and password).
2. **Token Generation**: Upon successful authentication, the server generates a JWT and sends it back to the client.
3. **Token Storage**: The client stores the token (usually in local storage or a cookie).
4. **Sending the Token**: For subsequent requests, the client includes the JWT in the `Authorization` header as a Bearer token:
   ```plaintext
   Authorization: Bearer <token>
   ```
5. **Token Verification**: The server receives the token, verifies its signature and validity (expiration, etc.), and grants or denies access based on the claims contained in the token.

### **Benefits of Using JWT for Authentication**

- **Compact**: JWTs are small in size and can be sent through URL, POST parameters, or HTTP headers.
- **Self-Contained**: JWTs contain all the information needed for authentication, reducing the need to query the database for user information on every request.
- **Cross-Domain**: JWTs can be used across different domains, making them suitable for Single Sign-On (SSO) scenarios.
- **Stateless**: No need to maintain session state on the server, as all the necessary information is stored in the token itself.

### **Parties Involved in Setting Up JWT Authentication**

1. **Resource Owner (User)**:

   - The individual who wants to access a protected resource (e.g., a web application or API).

2. **Client Application**:

   - The application (web or mobile) that the resource owner uses to access the protected resources. The client sends authentication requests and includes the JWT in subsequent requests.

3. **Authorization Server**:

   - The server responsible for authenticating the resource owner (user) and issuing the JWT upon successful authentication. This server validates user credentials, generates the JWT, and sends it back to the client.

4. **Resource Server**:
   - The server hosting the protected resources (APIs or services). It receives requests from the client with the JWT and verifies the token before granting access to the requested resources.

### **Workflow of JWT Authentication**

1. **Login**: The user submits their credentials (username and password) to the client application.
2. **Token Request**: The client sends a request to the authorization server for a JWT.
3. **Token Generation**: The authorization server validates the credentials and generates a JWT containing user claims.
4. **Token Response**: The authorization server sends the JWT back to the client application.
5. **Token Storage**: The client stores the JWT (usually in local storage or cookies).
6. **Resource Request**: For subsequent requests to the resource server, the client includes the JWT in the Authorization header.
7. **Token Verification**: The resource server verifies the JWT, checking its signature, expiration, and claims.
8. **Access Granted/Denied**: If the JWT is valid, the resource server processes the request; otherwise, it returns an error (e.g., 401 Unauthorized).

### **Conclusion**

JWT is a widely adopted standard for handling authentication in modern web applications. Its compact and self-contained structure, along with the separation of concerns among the parties involved (resource owner, client, authorization server, and resource server), make it a robust solution for implementing secure authentication mechanisms. By using JWT, developers can create stateless, scalable, and cross-domain applications with ease.
