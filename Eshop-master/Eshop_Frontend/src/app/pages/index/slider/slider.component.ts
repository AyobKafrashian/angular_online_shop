import {Component, OnInit} from '@angular/core';
import {SliderService} from '../../../services/slider.service';
import {Slider} from '../../../DTOs/Sliders/Slider';
import {DomainName} from 'src/app/Utilities/PathTools';

declare function HomeSlider():any;

@Component({
  selector: 'app-index-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.scss']
})
export class SliderComponent implements OnInit {

  //#region Importnent
  public sliders: Slider[] = [];
  public domain:string = DomainName;
  //#endregion

  constructor(
    private sliderService: SliderService
  ) {
  }

  ngOnInit(): void {

    this.sliderService.getCurrentSliders().subscribe(sliders => {
      if (sliders === null) {
        this.sliderService.GetSliders().subscribe(res => {
          if (res.status === 'Success') {
            this.sliderService.setCurrentSliders(res.data);
          }
        });
      } else {
        this.sliders = sliders; 
        setInterval(()=>{
          HomeSlider();
        },100);
      }
    });
  }
}
