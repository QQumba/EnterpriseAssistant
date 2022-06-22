import http from 'k6/http';
import { check, sleep } from 'k6';


const API_URL = 'https://localhost:5002/api/'; 
const BEARER = 'eyJhbGciOiJSUzI1NiIsImtpZCI6IkNDMENFRjc4MDNGNTY0QUI4MDU2NDc1ODNFMkY5N0E0IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NTU4MjU5MTMsImV4cCI6MTY1NTgyOTUxMywiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwNCIsImNsaWVudF9pZCI6ImVhLnNwYSIsInN1YiI6InRlc3RAbWFpbC5jb20iLCJhdXRoX3RpbWUiOjE2NTU4MDQ4ODIsImlkcCI6ImxvY2FsIiwidXNlcl9pZCI6IjEiLCJlbWFpbCI6InRlc3RAbWFpbC5jb20iLCJmaXJzdF9uYW1lIjoiTXlreXRhIiwibGFzdF9uYW1lIjoiS255c2giLCJlbnRlcnByaXNlX2lkcyI6Im15RW50ZXJwcmlzZSIsImp0aSI6IjdGMDBDQTYyNDg0QkE3OTBBQzcxQjQ2MUFBNEM4NjI3Iiwic2lkIjoiMTUzNTQwNDVGOURGQUUzNDFFNjIwNjk3OEQ5QTlDNDciLCJpYXQiOjE2NTU4MjU5MTMsInNjb3BlIjpbIm9wZW5pZCIsInByb2ZpbGUiLCJlYSJdLCJhbXIiOlsicHdkIl19.jgx_qDGdfLW1gJSXS2wIrNQISOkzX-XDZi2PGjJpGgkyny8Flz-bVRouEw0z3WbVv02Rp8ELtzt2gESWUTlG_aTdXhKI9ZkiNcCkISDhJWGXbLs6E6JYKit_IfqXOnFOf9xb_dlkJoE5pNkerzJ_TUOpH-NKP8rHGjQGS8Vq30YVfId1L2moSjJkCxkpXnTGvdXQ0HDZ6-3X3M4K9nSelrXn2fvUBiAPbzo4X10fLmXSq-OfunncCzJFhHPRnZ5hwjDhNsNHQdNQvQV6ODNKSeMM5xE3WmFC11gVahlygagFaqDbrD0WGn2tjcqzXmEJgzoGsXdOBdv3vHFgkSY5wA';
const enterpriseId = 'myEnterprise';
const authHeaders = {
  headers: {
    Authorization: `Bearer ${BEARER}`,
    'auth-enterprise': enterpriseId
  },
}
;
export const options = {
  stages: [
    { duration: '1s', target: 1000 },
    { duration: '29s', target: 1000 },
  ],
};

export default function () {
  const users = http.get(API_URL + 'enterprise/user', authHeaders).json();
  check(users, { 'retrieved users': (obj) => obj.length > 0 });
  sleep(1);
}