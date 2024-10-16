import { Options, Vue } from "vue-class-component";
import { IQuestionType, QuestionService } from '../../services/QuestionService';
import { ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
@Options({
    props: {

    },
    components: {

    }
})
export default class AskedQuestion extends Vue {
    questionService = new QuestionService();
    questionId: number;
    questionTitle: string = '';
    title = '';
    content = localStorage.getItem("chatQuestionDetail") || '';
    typeValue: { label: string, value: string, id: number } = { label: '', value: '', id: null };
    options = [];
    isAdd = true;
    router = useRouter();
    familyValue: string[] | string;
    familyOptions = [];
    checkedPrivate = false;
    productId;
    userId: number;
    created() {
        localStorage.removeItem("chatQuestionDetail");
        this.userId = Number(localStorage.getItem('userId'));
        let route = useRoute();
        this.familyValue = route.query?.family;
        if (route.query?.id) {
            this.productId = Number(route.query?.id);
            debugger;
        }
        if (route.query?.questionId) {
            this.questionId = Number(route.query?.questionId);
            debugger;
        }
        this.questionService.getAllQuestionTypes().then(res => {
            this.options = res.data.map(x => ({ label: x.type, value: x.id.toString(), id: x.id, familyName: x.family.name, familyId: x.family.id }));
            this.typeValue = this.options.length > 0 && this.options[0];
            this.familyOptions = res.data.map(x => ({ label: x.family.name, value: x.family.id.toString(), id: x.family.id }));
            this.familyValue = this.familyOptions.length > 0 && this.familyOptions[0];
        });
        if (this.questionId) {
            this.isAdd = false;
            this.questionService.getQuestionDetail(this.questionId).then(res => {
                this.title = res.data.title;
                this.content = res.data.content;
                this.typeValue = this.options.find(x => x.id == res.data.questionTypeId);
                this.checkedPrivate = res.data.isPrivate;
            });
        }


    }
    submit() {
        let request = { userId: this.userId, typeId: this.typeValue.id, title: this.title, content: this.content, productId: this.productId, SupportId: 5, isPrivate: this.checkedPrivate }
        this.questionService.addOrUpdateQuestion(request).then(res => {
            console.log(res);
            this.router.push('/ai/' + res.data.id);
            debugger;
        });
    }
    cancel() {
        this.router.push('/ai/' + this.questionId);
    }
    confirm() {
        let request = { userId: this.userId, id: this.questionId, typeId: this.typeValue.id, title: this.title, content: this.content, productId: this.productId, isPrivate: this.checkedPrivate }
        this.questionService.addOrUpdateQuestion(request).then(res => {
            console.log(res);
            this.router.push('/ai/' + this.questionId);
        });
    }
}