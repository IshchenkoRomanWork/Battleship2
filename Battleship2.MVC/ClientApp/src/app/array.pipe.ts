import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'array'
})
export class ArrayPipe implements PipeTransform {

  transform(value: number, args?: any): any {
    return new Array(value);
  }

}
