import { Component, Input } from '@angular/core';
import { User } from '../../models/user.model';

const URL = 'https://ui-avatars.com/api/?format=svg';

@Component({
  selector: 'app-avatar',
  templateUrl: './avatar.component.html',
  styleUrls: ['./avatar.component.scss']
})
export class AvatarComponent {
  @Input() user!: User;

  getAvatarUrl(): string {
    let name = this.user.firstName;
    if (this.user.lastName) {
      name += '+' + this.user.lastName;
    }
    return `${URL}&background=random&format=svg&name=${name}`;
  }
}
