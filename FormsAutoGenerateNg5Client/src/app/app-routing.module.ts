import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainWindowComponent } from './main-window/main-window.component';

const routes: Routes = [
  {
    path: '',
    component: MainWindowComponent
  },
  {
    path: ':tablename',
    component: MainWindowComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
