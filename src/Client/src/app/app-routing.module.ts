import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EnterpriseCreateEnterpriseComponent } from './components/enterprise/enterprise-create-enterprise/enterprise-create-enterprise.component';
import { StartComponent } from './components/start/start.component';

const routes: Routes = [
  { path: '', component: StartComponent },
  { path: 'create-enterprise', component: EnterpriseCreateEnterpriseComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
