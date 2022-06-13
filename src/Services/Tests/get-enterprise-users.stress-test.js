import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  stages: [
    { duration: '2s', target: 100 }, // below normal load
    { duration: '18s', target: 100 }, // below normal load
  ],
};

const API_URL = 'https://localhost:5002/api/'; 
const BEARER = 'eyJhbGciOiJSUzI1NiIsImtpZCI6IkNDMENFRjc4MDNGNTY0QUI4MDU2NDc1ODNFMkY5N0E0IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NTUwOTE0OTYsImV4cCI6MTY1NTA5NTA5NiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwNCIsImNsaWVudF9pZCI6ImVhLnNwYSIsInN1YiI6Im5ld0B1c2VyLmNvbSIsImF1dGhfdGltZSI6MTY1NDk1MjEwOCwiaWRwIjoibG9jYWwiLCJ1c2VyX2lkIjoiNCIsImVtYWlsIjoibmV3QHVzZXIuY29tIiwibmFtZSI6Ik5pY2siLCJlbnRlcnByaXNlX2lkcyI6InN0cmluZyBuaWNrLXN1cGVyLWVudGVycHJpc2UiLCJqdGkiOiI1QUMxMUQ0NTU0NTJBM0MxMDAyRkY3NDQyOTk2QTQzOCIsInNpZCI6IkFENjY2REQ0RDNFMDMwODREOUEzQzExQTc1MEU5RTQ1IiwiaWF0IjoxNjU1MDkxNDk2LCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZWEiXSwiYW1yIjpbInB3ZCJdfQ.bvmiMUfNbym7jebwF1RMK5lMnksGmEzcFFgwzNluREb_K7MZFWVRd4UpujraD0scOAStTNRgpqeFjrjzSHA2AyJ-fJdI_iRN22lhhvP367O_z--3cOoLM5nDufW3p24EhyepfntKZkgh9iVzsfGUHPnSCFRUoZUp422Cwf7QrK_DuiRtMZa2j53r5w8ngwdxx2Qu7lDtP-YWno-vSRm-FvMTp-k-nNKQxe-hE1102Sts0xJbDX7rGOnvmBqzoreNaJKQ88B-8Zt2E-X6rWKPtpN9kftREDerIgwmPlmuToq_zYle_4sIZ2NfA7Zt34mC8QFp0DwyEFbpIfp-IjZn2Q';
const enterpriseId = 'nick-super-enterprise';
const authHeaders = {
  headers: {
    Authorization: `Bearer ${BEARER}`,
    'auth-enterprise': enterpriseId
  },
};

export default function () {
  const users = http.get(API_URL + 'enterprise/user', authHeaders).json();
  check(users, { 'retrieved users': (obj) => obj.length > 0 });
  sleep(1);
}