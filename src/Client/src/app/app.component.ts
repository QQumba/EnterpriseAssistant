import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

const DefaultLang = 'en';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'enterprise-assistant';

  constructor(private translate: TranslateService) {
    translate.setDefaultLang(DefaultLang);
    translate.use(DefaultLang);
  }
}
