import { Injectable } from '@angular/core';
import { User } from '../models/user.model';

const URL = 'https://ui-avatars.com/api/?format=svg';

@Injectable({
  providedIn: 'root'
})
export class AvatarService {
  getAvatarUrl(user: User): string {
    let name = user.firstName;
    if (user.lastName) {
      name += '+' + user.lastName;
    }
    return `${URL}&background=random&format=svg&name=${name}`;
  }
}
