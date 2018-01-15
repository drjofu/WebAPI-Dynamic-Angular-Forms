import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DynamicFormComponent } from './dynamic-form/dynamic-form.component';
import { DynamicFormPropertyComponent } from './dynamic-form-property/dynamic-form-property.component';
import { PropertyDescriptorConverterService } from './property-descriptor-converter.service';
import { DateValueAccessor } from '../utilities/date-value-accessor';

@NgModule({
  imports: [
    CommonModule, FormsModule,
    ReactiveFormsModule],
  providers: [PropertyDescriptorConverterService],
  declarations: [DynamicFormComponent, DynamicFormPropertyComponent, DateValueAccessor],
  exports: [DynamicFormComponent, DynamicFormPropertyComponent]
})
export class DynamicFormsModule { }
