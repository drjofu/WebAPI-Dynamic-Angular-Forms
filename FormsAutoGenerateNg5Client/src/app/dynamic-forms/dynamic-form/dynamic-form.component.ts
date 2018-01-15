import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PropertyDescriptorBase } from '../property-descriptor-base';
import { FormGroup } from '@angular/forms';
import { PropertyDescriptorConverterService } from '../property-descriptor-converter.service';

// Dynamisch generierter Editor für ausgewähltes Objekt
@Component({
  selector: 'dynamic-form',
  templateUrl: './dynamic-form.component.html',
  styleUrls: ['./dynamic-form.component.css']
})
export class DynamicFormComponent implements OnInit {

  // Metadaten für Objekt-Properties
  @Input() propertyDescriptors: PropertyDescriptorBase[] = [];

  // Das zu ändernde Objekt
  @Input() data: object;

  // Event für Fertigmeldung
  @Output() saveData = new EventEmitter<object>();

  // Event für Abbruchmeldung
  @Output() cancelled = new EventEmitter();

  // FormGroup für Reactive-Form-Implementierung
  form: FormGroup;

  constructor(private pdcs: PropertyDescriptorConverterService) { }

  ngOnInit() {
    // FormGroup basierend auf den Metadaten erstellen
    this.form = this.pdcs.toFormGroup(this.propertyDescriptors, this.data);
  }

  onSubmit() {
    this.saveData.emit(this.form.value);
  }

  cancel() {
    this.cancelled.emit();
  }
}
