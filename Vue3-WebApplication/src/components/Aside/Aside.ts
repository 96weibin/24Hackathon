import { Vue, Options } from 'vue-class-component';
import { Tabs } from "../../services/homeService";
import { useRoute, useRouter } from 'vue-router';
// import router from '../../router';
  
@Options({
    emits: ['changeAside']  //$emit 事件
  })
export default class Aside extends Vue{
    public Tabs = Tabs;
    public isCollapse: boolean = true;
    private router = useRouter();
    handleSelect(key: string, keyPath: string[]){
        switch (Number.parseInt(key)) {
            case Tabs.Summary:
                this.router.push('/summary');
                break;
            case Tabs.Charts:
                this.router.push('/charts');
                break;
            case Tabs.Grid:
                this.router.push('/grid')
                break;
            default:
                break;
        }            
    }
    onExpand(){
        this.isCollapse = !this.isCollapse
    }
}