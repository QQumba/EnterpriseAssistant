export interface Invite {
  enterpriseId: string;
  enterpriseDisplayedName: string;
  userEmail: string;
  status: InviteStatus;
}

export interface InviteCreate {
  userEmail: string;
}

export enum InviteStatus {
  Pending,
  Accepted
}
