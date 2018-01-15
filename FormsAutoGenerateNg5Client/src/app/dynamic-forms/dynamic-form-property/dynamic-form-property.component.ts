import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { PropertyDescriptorBase } from '../property-descriptor-base';

// Eingabekomponente für eine einzelne Property
@Component({
  selector: 'dynamic-form-property',
  templateUrl: './dynamic-form-property.component.html',
  styleUrls: ['./dynamic-form-property.component.css']
})
export class DynamicFormPropertyComponent implements OnInit {

  // Metadaten für diese Property
  @Input() propertyDescriptor: PropertyDescriptorBase;

  // FormGroup-Objekt für dieses Control
  @Input() form: FormGroup;

  // Gültigkeitsprüfung
  get isValid() { return this.form.controls[this.propertyDescriptor.name].valid; }

  constructor() { }
 
  ngOnInit() {
  }

}
