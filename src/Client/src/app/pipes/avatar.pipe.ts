import { Pipe, PipeTransform } from '@angular/core';
import { User } from '../models/user.model';
import { AvatarService } from '../services/avatar.service';

@Pipe({
  name: 'avatar'
})
export class AvatarPipe implements PipeTransform {
  constructor(private service: AvatarService) {}

  transform(value: User): unknown {
    return this.service.getAvatarUrl(value);
  }
}
